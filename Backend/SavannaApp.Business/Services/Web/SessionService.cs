using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Helpers.Extensions;
using SavannaApp.Data.Interfaces.Repo;

namespace SavannaApp.Business.Services.Web
{
    public class SessionService(ISessionRepository sessionRepository) : ISessionService
    {
        /// <summary>
        /// Method for creating session
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="userId">user id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <param name="expiresAt">expires at </param>
        /// <returns>created session</returns>
        public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            await sessionRepository.CreateSessionAsync(sessionId, userId, refreshToken, expiresAt);
        }

        /// <summary>
        /// Method to extend session
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <param name="expiresAt">time to extend to</param>
        /// <returns>extended session</returns>
        public async Task ExtendSessionsAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            await sessionRepository.ExtendSessionsAsync(sessionId, refreshToken, expiresAt);
        }

        /// <summary>
        /// Method for session invalidation
        /// </summary>
        /// <param name="sessionId">session id to invalidate</param>
        /// <returns>invalidated session</returns>
        public async Task InvalidateSessionAsync(Guid sessionId)
        {
            await sessionRepository.InvalidateSessionAsync(sessionId);
        }

        /// <summary>
        /// Check whether sessions is valid
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <returns>true if valid, false if not</returns>
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
