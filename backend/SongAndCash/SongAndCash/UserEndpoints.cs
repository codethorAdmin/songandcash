using System.Text.Json;
using SongAndCash.Model.Dto;
using SongAndCash.Service.Business;
using SongAndCash.Service.Mapper;

namespace SongAndCash;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapGet(
            "/users/{userId}",
            async (int userId, IUserService userService, IUserMapper userMapper) =>
            {
                var user = await userService.GetUser(userId);
                var userDto = userMapper.MapToUserDto(user);

                return Results.Ok(userDto);
            }
        );

        app.MapPost(
            "/users",
            async (HttpContext context, IUserService userService, IUserMapper userMapper) =>
            {
                using var reader = new StreamReader(context.Request.Body);
                var bodyText = await reader.ReadToEndAsync();
                var createUserDto = JsonSerializer.Deserialize<CreateUserDto>(bodyText);

                if (createUserDto == null)
                {
                    return Results.BadRequest();
                }

                var createdUser = await userService.CreateUser(
                    userMapper.MapToCreateUser(createUserDto)
                );

                return Results.Created($"/users/{createdUser.Id}", createdUser);
            }
        );

        app.MapPut(
            "/users/{userId}",
            async (
                int userId,
                HttpContext context,
                IUserService userService,
                IUserMapper userMapper
            ) =>
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
            }
        );

        return app;
    }
}
