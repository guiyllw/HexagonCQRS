using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class ReadContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public ReadContext(DbContextOptions<ReadContext> options) : base(options) { }
    }
}
