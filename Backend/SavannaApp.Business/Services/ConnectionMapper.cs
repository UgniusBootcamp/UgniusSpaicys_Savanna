using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SavannaApp.Business.Services
{
    public static class ConnectionMapper
    {
        private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();

        public static void Add(string userId, string connectionId)
        {
            lock (_connections)
            {
                _connections[userId] = connectionId;
            }
        }

        public static void Remove(string userId)
        {
            lock (_connections)
            {
                _connections.Remove(userId);
            }
        }

        public static string? GetConnectionId(string userId)
        {
            lock (_connections)
            {
                return _connections.TryGetValue(userId, out var connectionId)
                    ? connectionId
                    : null;
            }
        }

        public static bool ConnectionExists(string userId)
        {
            lock (_connections)
            {
                return _connections.Keys.Contains(userId);
            }
        }
    }
}
