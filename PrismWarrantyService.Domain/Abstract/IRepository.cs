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
        IQueryable<Employee> Employees { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Performer> Performers { get; }
        IQueryable<Priority> Priorities { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<State> States { get; }
  
        #endregion

        #region Methods

        // Orders CRUD
        void CreateOrder(Order order);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);

        // Clients CRUD
        void CreateClient(Client client);
        void DeleteClient(Client client);
        void UpdateClient(Client client);


        // Companies CRUD
        void CreateCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);

        #endregion
    }
}
