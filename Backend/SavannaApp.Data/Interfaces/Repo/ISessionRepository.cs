using SavannaApp.Data.Entities.Auth;

namespace SavannaApp.Data.Interfaces.Repo
{
    public interface ISessionRepository
    {
        Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt);
        Task ExtendSessionsAsync(Guid sessionId, string refreshToken, DateTime expiresAt);
        Task<Session?> GetSessionByIdAsync(Guid sessionId);
        Task InvalidateSessionAsync(Guid sessionId);
    }
}
