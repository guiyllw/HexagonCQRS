using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Contexts
{
    public class ReadContext : DbContext
    {
        public DbSet<Order.Entities.Order> Orders { get; set; }

        public ReadContext(DbContextOptions<ReadContext> options) : base(options) { }
    }
}
