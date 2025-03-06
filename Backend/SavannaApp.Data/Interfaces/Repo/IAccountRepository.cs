using SavannaApp.Data.Entities.Auth;

namespace SavannaApp.Data.Interfaces.Repo
{
    public interface IAccountRepository
    {
        Task<User?> CreateUserAsync(User user, string password, string roleId);
        Task<User?> FindUserByIdAsync(string userId);
        Task<User?> FindUserByUsernameAsync(string username);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<bool> IsPasswordValidAsync(User user, string password);
        Task<bool> IsUserInRoleAsync(User user, List<string> allowerdRoles);
    }
}
