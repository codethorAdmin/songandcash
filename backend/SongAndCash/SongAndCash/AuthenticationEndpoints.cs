using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SongAndCash.Model.Configuration;
using SongAndCash.Service.Business;

namespace SongAndCash;

public static class AuthenticationEndpoints
{
    public static WebApplication MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapGet(
            "/auth/login",
            () =>
                Results.Challenge(
                    new AuthenticationProperties { RedirectUri = "/auth/callback" },
                    [GoogleDefaults.AuthenticationScheme]
                )
        );

        app.MapGet(
            "/auth/callback",
            async (
                HttpContext context,
                IOptions<GlobalConfiguration> configuration,
                IUserService userService
            ) =>
            {
                var authenticateResult = await context.AuthenticateAsync(
                    GoogleDefaults.AuthenticationScheme
                );

                if (!authenticateResult.Succeeded)
                {
                    return Results.Unauthorized();
                }

                var token = authenticateResult.Properties?.Items["jwt_token"];

                if (!string.IsNullOrEmpty(token))
                {
                    _ = await userService.LoginOrRegister(authenticateResult);
                    return Results.Redirect(
                        $"{configuration.Value.UrlApp}/auth/callback?token={token}"
                    );
                }

                return Results.Redirect($"{configuration.Value.UrlApp}/auth/error");
            }
        );

        app.MapGet(
            "/api/userinfo",
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (HttpContext context, IUserService userService) =>
            {
                var username = context
                    .User.Claims?.FirstOrDefault(x =>
                        x.Type
                        == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
                    )
                    ?.Value;
                if (string.IsNullOrEmpty(username))
                {
                    return Results.Unauthorized();
                }

                var user = await userService.GetUserByUsername(username);
                return Results.Ok(
                    new
                    {
                        Email = user.Username,
                        Name = user.Email,
                        UserId = user.Id,
                    }
                );
            }
        );

        return app;
    }
}
