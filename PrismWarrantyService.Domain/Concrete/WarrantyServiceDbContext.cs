using System.Data.Entity;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.Domain.Concrete
{
    public class WarrantyServiceDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Employees)
                .WithMany(e => e.Orders)
                .Map(m =>
                {
                    m.ToTable("Performers");
                    m.MapLeftKey("OrderID");
                    m.MapRightKey("EmployeeID");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}