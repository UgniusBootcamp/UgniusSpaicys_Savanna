using Microsoft.EntityFrameworkCore;
using SavannaApp.Data.Data;
using SavannaApp.Data.Entities.Auth;
using SavannaApp.Data.Helpers.Exceptions;
using SavannaApp.Data.Helpers.Extensions;
using SavannaApp.Data.Interfaces.Repo;

namespace SavannaApp.Data.Repositories
{
    public class SessionRepository(SavannaDbContext context) : ISessionRepository
    {
        public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            var session = new Session
            {
                Id = sessionId,
                UserId = userId,
                InitiatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt,
                LastRefreshToken = refreshToken.ToSha256(),
            };

            context.Sessions.Add(session);

            await context.SaveChangesAsync();
        }

        public async Task ExtendSessionsAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            var session = context.Sessions.FirstOrDefault(s => s.Id == sessionId) ?? throw new NotFoundException(String.Format("Sessions with id {0} was not found", sessionId));

            session.ExpiresAt = expiresAt;
            session.LastRefreshToken = refreshToken.ToSha256();

            await context.SaveChangesAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(Guid sessionId)
        {
            return await context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task InvalidateSessionAsync(Guid sessionId)
        {
            var session = context.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session == null) return;

            session.IsRevoked = true;

            context.Sessions.Update(session);

            await context.SaveChangesAsync();
        }
    }
}
