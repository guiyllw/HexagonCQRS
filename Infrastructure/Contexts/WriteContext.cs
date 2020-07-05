using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class WriteContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public WriteContext(DbContextOptions<WriteContext> options) : base(options) { }
    }
}
