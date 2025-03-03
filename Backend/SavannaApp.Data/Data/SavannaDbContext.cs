using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SavannaApp.Data.Entities.Auth;

namespace SavannaApp.Data.Data
{
    public class SavannaDbContext : IdentityDbContext<User>
    {
        public SavannaDbContext(DbContextOptions<SavannaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
