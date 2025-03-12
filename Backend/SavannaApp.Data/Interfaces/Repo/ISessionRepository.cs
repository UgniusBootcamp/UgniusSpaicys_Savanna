using SavannaApp.Data.Entities.Auth;

namespace SavannaApp.Data.Interfaces.Repo
{
    public interface ISessionRepository
    {
        /// <summary>
        /// Method to create session
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="userId">user id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <param name="expiresAt">expires at date</param>
        /// <returns>created session</returns>
        Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt);

        /// <summary>
        /// Method to extend session
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <param name="expiresAt">expires at date</param>
        /// <returns>extended session</returns>
        Task ExtendSessionsAsync(Guid sessionId, string refreshToken, DateTime expiresAt);

        /// <summary>
        /// Method to get session by its id
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <returns>session</returns>
        Task<Session?> GetSessionByIdAsync(Guid sessionId);

        /// <summary>
        /// Method to invalidate session
        /// </summary>
        /// <param name="sessionId">sesssion id</param>
        /// <returns>session</returns>
        Task InvalidateSessionAsync(Guid sessionId);
    }
}
