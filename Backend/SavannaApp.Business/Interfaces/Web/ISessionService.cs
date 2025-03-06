namespace SavannaApp.Business.Interfaces.Web
{
    public interface ISessionService
    {
        Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt);
        Task ExtendSessionsAsync(Guid sessionId, string refreshToken, DateTime expiresAt);
        Task InvalidateSessionAsync(Guid sessionId);
        Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken);
    }
}
