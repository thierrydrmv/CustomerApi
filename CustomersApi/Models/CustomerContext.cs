using Microsoft.EntityFrameworkCore;

namespace CustomersApi.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> CustomerItems { get; set; } = null!;
    }
}
