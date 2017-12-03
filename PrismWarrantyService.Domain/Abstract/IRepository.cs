using System.Linq;
using PrismWarrantyService.Domain.Entities;
using System.Collections.Generic;

namespace PrismWarrantyService.Domain.Abstract
{
    public interface IRepository
    {
        #region Properties

        IQueryable<Client> Clients { get; }
        IQueryable<Company> Companies { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Performer> Performers { get; }
        IQueryable<Priority> Priorities { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<State> States { get; }
  
        #endregion

        #region Methods

        void AddOrder(Order order);
        void DeleteOrder(Order order);
        void EditOrder(Order order);

        #endregion
    }
}
