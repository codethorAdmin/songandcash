using System.Security.Claims;

namespace SongAndCash.Service.Business;

public interface ITokenService
{
    string GenerateJwtToken(ClaimsPrincipal user, GenerateJwtTokenOptions options);
}
