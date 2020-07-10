using Infrastructure.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Common.Contexts
{
    public class WriteContext : DbContext
    {
        public DbSet<Order.Entities.Order> Orders { get; set; }

        public WriteContext(DbContextOptions<WriteContext> options) : base(options) { }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = ChangeTracker
                .Entries()
                .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified);

            var now = DateTime.UtcNow;
            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                }

                entity.UpdatedDate = now;
            }

            return base.SaveChanges();
        }
    }
}
