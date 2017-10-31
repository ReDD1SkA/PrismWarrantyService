using System.Data.Entity;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.Domain.Concrete
{
    public class WarrantyServiceDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}