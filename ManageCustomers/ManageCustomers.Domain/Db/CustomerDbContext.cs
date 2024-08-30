using ManageCustomers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageCustomers.Domain
{
    public class CustomerDbContext : DbContext
    {

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ManageCustomers");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
