using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SavannaApp.Business.Services
{
    public static class ConnectionMapper
    {
        private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();

        /// <summary>
        /// Add user connection
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="connectionId">connection id</param>
        public static void Add(string userId, string connectionId)
        {
            lock (_connections)
            {
                _connections[userId] = connectionId;
            }
        }

        /// <summary>
        /// Method to remove user connection
        /// </summary>
        /// <param name="userId">user id</param>
        public static void Remove(string userId)
        {
            lock (_connections)
            {
                _connections.Remove(userId);
            }
        }

        /// <summary>
        /// Get connection id from user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>connection id</returns>
        public static string? GetConnectionId(string userId)
        {
            lock (_connections)
            {
                return _connections.TryGetValue(userId, out var connectionId)
                    ? connectionId
                    : null;
            }
        }

        /// <summary>
        /// Check if connection exists
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>true if exists, false if not</returns>
        public static bool ConnectionExists(string userId)
        {
            lock (_connections)
            {
                return _connections.Keys.Contains(userId);
            }
        }
    }
}
