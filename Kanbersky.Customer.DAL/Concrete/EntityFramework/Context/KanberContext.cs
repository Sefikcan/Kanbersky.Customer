using Microsoft.EntityFrameworkCore;

namespace Kanbersky.Customer.DAL.Concrete.EntityFramework.Context
{
    public class KanberContext : DbContext
    {
        public KanberContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entity.Concrete.Customer> Customers { get; set; }

        public DbSet<Entity.Concrete.Address> Addresses { get; set; }
    }
}
