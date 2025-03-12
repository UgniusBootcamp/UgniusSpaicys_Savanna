namespace SavannaApp.Business.Interfaces.Web
{
    public interface ISessionService
    {
        /// <summary>
        /// Method for creating session
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="userId">user id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <param name="expiresAt">expires at </param>
        /// <returns>created session</returns>
        Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt);

        /// <summary>
        /// Method to extend session
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <param name="expiresAt">time to extend to</param>
        /// <returns>extended session</returns>
        Task ExtendSessionsAsync(Guid sessionId, string refreshToken, DateTime expiresAt);

        /// <summary>
        /// Method for session invalidation
        /// </summary>
        /// <param name="sessionId">session id to invalidate</param>
        /// <returns>invalidated session</returns>
        Task InvalidateSessionAsync(Guid sessionId);

        /// <summary>
        /// Check whether sessions is valid
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="refreshToken">refresh token</param>
        /// <returns>true if valid, false if not</returns>
        Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken);
    }
}
