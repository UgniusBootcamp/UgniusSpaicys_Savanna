using SavannaApp.Data.Entities.Auth;

namespace SavannaApp.Data.Interfaces.Repo
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Method to create user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="password">password</param>
        /// <param name="roleId">role</param>
        /// <returns>created user</returns>
        Task<User?> CreateUserAsync(User user, string password, string roleId);

        /// <summary>
        /// Method to find user by id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>user</returns>
        Task<User?> FindUserByIdAsync(string userId);

        /// <summary>
        /// Method to find user by username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user</returns>
        Task<User?> FindUserByUsernameAsync(string username);

        /// <summary>
        /// Method to get user roles
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>user roles/returns>
        Task<IList<string>> GetUserRolesAsync(User user);

        /// <summary>
        /// Method to validate password
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="password">password</param>
        /// <returns>true if valid, false if not</returns>
        Task<bool> IsPasswordValidAsync(User user, string password);

        /// <summary>
        /// Method to check if user is in role
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="allowerdRoles">allowed roles</param>
        /// <returns>true if in role, false if not</returns>
        Task<bool> IsUserInRoleAsync(User user, List<string> allowerdRoles);
    }
}
