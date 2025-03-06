using Microsoft.AspNetCore.Identity;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Data;
using SavannaApp.Data.Entities.Auth;
using SavannaApp.Data.Helpers.Exceptions;
using SavannaApp.Data.Interfaces.Repo;

namespace SavannaApp.Data.Repositories
{
    public class AccountRepository(
        SavannaDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
        ) : IAccountRepository
    {
        public async Task<User?> CreateUserAsync(User user, string password, string roleId)
        {
            await using var transaction = context.Database.BeginTransaction();

            try
            {
                var createUserResult = await userManager.CreateAsync(user, password);
                if (!createUserResult.Succeeded)
                    throw new Exception(createUserResult.Errors.First().Description);

                var role = await roleManager.FindByNameAsync(roleId);
                if (role == null)
                    throw new NotFoundException(RepoConstants.RoleNotFound);

                var addRoleResult = await userManager.AddToRoleAsync(user, role.Name!);
                if (!addRoleResult.Succeeded)
                    throw new Exception(addRoleResult.Errors.First().Description);

                await transaction.CommitAsync();

                return user;
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                
                switch (ex)
                {
                    case NotFoundException _:
                        throw;
                    default:
                        throw new Exception(String.Format(RepoConstants.FailedToCreateUser, ex.Message));
                }
            }
        }

        public async Task<User?> FindUserByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public async Task<User?> FindUserByUsernameAsync(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsPasswordValidAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> IsUserInRoleAsync(User user, List<string> allowerdRoles)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            foreach (var role in allowerdRoles)
            {
                if (await userManager.IsInRoleAsync(user, role))
                    return true;
            }

            return false;
        }
    }
}
