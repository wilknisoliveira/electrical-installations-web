using ei_back.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace ei_back.Infrastructure.Context
{
    public class PostgresContext : DbContext
    {
        public PostgresContext() { }

        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
