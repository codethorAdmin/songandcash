using SongAndCash;
using SongAndCash.Model.Configuration;
using SongAndCash.Repository;
using SongAndCash.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();

builder.Services.Configure<GlobalConfiguration>(builder.Configuration);

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

app.MapUserEndpoints().MapRecoverableSalesEndpoints();

app.Run();
