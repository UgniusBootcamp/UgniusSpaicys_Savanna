using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SavannaApp.Data.Entities.Auth;

namespace SavannaApp.Data.Data
{
    public class SavannaDbContext : IdentityDbContext<User>
    {
        public SavannaDbContext(DbContextOptions<SavannaDbContext> options) : base(options) { }

        public DbSet<Session> Sessions { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Sessions)
                    .WithOne(s => s.User)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
