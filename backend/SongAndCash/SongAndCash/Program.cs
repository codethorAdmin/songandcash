using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SongAndCash;
using SongAndCash.Model.Configuration;
using SongAndCash.Repository;
using SongAndCash.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();
builder.Services.Configure<GlobalConfiguration>(builder.Configuration);

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = "Cookies"; // Use a cookie-based scheme for sign-ins
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

        options.Events.OnTicketReceived = async (TicketReceivedContext context) =>
        {
            var claims = context.Principal?.Claims.ToList() ?? [];

            claims.Add(
                new System.Security.Claims.Claim(
                    "auth_time",
                    DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
                )
            );

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])
            );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: builder.Configuration["JWT:Issuer"],
                audience: builder.Configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            context.Properties!.Items["jwt_token"] = new JwtSecurityTokenHandler().WriteToken(
                token
            );
        };
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])
            ),
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReactApp",
        corsPolicyBuilder =>
            corsPolicyBuilder
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
    );
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SongAndCashContext>();
    await dbContext.Database.EnsureCreatedAsync();
    await DatabaseMigrator.MigrateDatabase(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exception = context
            .Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()
            ?.Error;

        context.Response.StatusCode = ExceptionHttpStatusCodeHandler.FromException(exception);

        var response = new
        {
            Message = "An error occurred while processing your request.",
            Details = exception?.Message, // Exclude or include in production as needed
        };

        await context.Response.WriteAsJsonAsync(response);
    });
});

app.MapAuthenticationEndpoints().MapUserEndpoints().MapRecoverableSalesEndpoints();

app.Run();
