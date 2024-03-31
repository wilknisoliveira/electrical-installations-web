using ei_back.Domain.Role;
using ei_back.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace ei_back.Infrastructure.Context
{
    public class EIContext : DbContext
    {
        public EIContext() { }

        public EIContext(DbContextOptions<EIContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            List<RoleEntity> roles =
            [
                new RoleEntity("Admin", ""),
                new RoleEntity("CommonUser", "")
            ];

            roles.ForEach(role => {
                role.Id = Guid.NewGuid();
                role.CreatedAt = DateTime.Now;
            });
            modelBuilder.Entity<RoleEntity>().HasData(roles);
        }
    }
}
