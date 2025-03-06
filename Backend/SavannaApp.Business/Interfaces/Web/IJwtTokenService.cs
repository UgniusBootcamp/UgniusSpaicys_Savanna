using System.Security.Claims;

namespace SavannaApp.Business.Interfaces.Web
{
    public interface IJwtTokenService
    {
        public string CreateAccessToken(string username, string userId, IEnumerable<string> roles);
        public string CreateRefreshToken(Guid sessionsId, string userId);
        public bool TryParseRefreshToken(string refreshToken, out ClaimsPrincipal? claims);
    }
}
