using SongAndCash;
using SongAndCash.Repository;
using SongAndCash.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();

var app = builder.Build();

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
