using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SongAndCash.Service.Business;

public class TokenService : ITokenService
{
    public string GenerateJwtToken(ClaimsPrincipal user, GenerateJwtTokenOptions options)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        try
        {
            var claims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.FindFirstValue(ClaimTypes.NameIdentifier)
                ),
                new Claim(JwtRegisteredClaimNames.Email, user.FindFirstValue(ClaimTypes.Email)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            throw new UnauthorizedAccessException(ex.Message);
        }
    }
}

public class GenerateJwtTokenOptions
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
