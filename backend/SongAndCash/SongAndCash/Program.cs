using System.Text.Json;
using SongAndCash.Model.Dto;
using SongAndCash.Service;
using SongAndCash.Service.Mapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.RegisterServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/users/{id}", async (int id, IUserService userService, IUserMapper userMapper) =>
{
    var user = await userService.GetUser(id);
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

app.MapPut("/users", async (int id, HttpContext context, IUserService userService, IUserMapper userMapper) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var bodyText = await reader.ReadToEndAsync();
    var updateUserDto = JsonSerializer.Deserialize<UpdateUserDto>(bodyText);

    if (updateUserDto == null)
    {
        return Results.BadRequest();
    }

    _ = await userService.UpdateUser(userMapper.MapToUpdateUser(updateUserDto));
    
    return Results.NoContent();
});

app.Run();