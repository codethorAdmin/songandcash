using System.Text.Json;
using SongAndCash;
using SongAndCash.Model.Dto;
using SongAndCash.Repository;
using SongAndCash.Service;
using SongAndCash.Service.Business;
using SongAndCash.Service.Mapper;

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

        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        context.Response.StatusCode = ExceptionHttpStatusCodeHandler.FromException(exception);

        var response = new
        {
            Message = "An error occurred while processing your request.",
            Details = exception?.Message // Exclude or include in production as needed
        };

        await context.Response.WriteAsJsonAsync(response);
    });
});

#region Users
app.MapGet("/users/{userId}", async (int userId, IUserService userService, IUserMapper userMapper) =>
{
    var user = await userService.GetUser(userId);
    var userDto = userMapper.MapToUserDto(user);
    
    return Results.Ok(userDto);
});

app.MapPost("/users", async (HttpContext context, IUserService userService, IUserMapper userMapper) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var bodyText = await reader.ReadToEndAsync();
    var createUserDto = JsonSerializer.Deserialize<CreateUserDto>(bodyText);

    if (createUserDto == null)
    {
        return Results.BadRequest();
    }

    var createdUser = await userService.CreateUser(userMapper.MapToCreateUser(createUserDto));
    
    return Results.Created($"/users/{createdUser.Id}", createdUser);
});

app.MapPut("/users/{userId}", async (int userId, HttpContext context, IUserService userService, IUserMapper userMapper) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var bodyText = await reader.ReadToEndAsync();
    var updateUserDto = JsonSerializer.Deserialize<UpdateUserDto>(bodyText);

    if (updateUserDto == null)
    {
        return Results.BadRequest();
    }

    _ = await userService.UpdateUser(userId, userMapper.MapToUpdateUser(updateUserDto));
    
    return Results.NoContent();
});
#endregion

#region Recoverable Sales

app.MapPost("/users/{userId}/recoverablesales/create", async (int userId, HttpContext context,
    IRecoverableSalesService recoverableSalesService, IRecoverableSalesMapper recoverableSalesMapper) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var bodyText = await reader.ReadToEndAsync();
    var createRecoverableSaleDto = JsonSerializer.Deserialize<CreateRecoverableSaleDto>(bodyText);

    if (createRecoverableSaleDto == null)
    {
        return Results.BadRequest();
    }
    
    var recoverableSale = 
        await recoverableSalesService.CreateRecoverableSale(recoverableSalesMapper.MapToCreateRecoverableSale(createRecoverableSaleDto));
    
    return Results.Created($"/users/{userId}/recoverablesales{recoverableSale.Id}", recoverableSale);
});

#endregion

app.Run();