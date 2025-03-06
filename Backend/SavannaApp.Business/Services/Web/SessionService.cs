using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Helpers.Extensions;
using SavannaApp.Data.Interfaces.Repo;

namespace SavannaApp.Business.Services.Web
{
    public class SessionService(ISessionRepository sessionRepository) : ISessionService
    {
        public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            await sessionRepository.CreateSessionAsync(sessionId, userId, refreshToken, expiresAt);
        }

        public async Task ExtendSessionsAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            await sessionRepository.ExtendSessionsAsync(sessionId, refreshToken, expiresAt);
        }

        public async Task InvalidateSessionAsync(Guid sessionId)
        {
            await sessionRepository.InvalidateSessionAsync(sessionId);
        }

        public async Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken)
        {
            var session = await sessionRepository.GetSessionByIdAsync(sessionId);

            Console.WriteLine(session.LastRefreshToken);
            Console.WriteLine(refreshToken.ToSha256());

            return session is not null && session.ExpiresAt > DateTime.UtcNow && !session.IsRevoked &&
                session.LastRefreshToken == refreshToken.ToSha256();
        }
    }
}
