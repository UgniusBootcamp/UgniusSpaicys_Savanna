using Microsoft.AspNetCore.Identity;

namespace SavannaApp.Data.Entities.Auth
{
    public class User : IdentityUser
    {
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
