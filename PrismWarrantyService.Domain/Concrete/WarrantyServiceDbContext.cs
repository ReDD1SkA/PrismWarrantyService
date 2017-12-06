using System.Data.Entity;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.Domain.Concrete
{
    public class WarrantyServiceDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
    }
}