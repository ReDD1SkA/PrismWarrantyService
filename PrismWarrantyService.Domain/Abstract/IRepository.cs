using System.Linq;
using PrismWarrantyService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismWarrantyService.Domain.Abstract
{
    public interface IRepository
    {
        #region Properties

        IQueryable<Client> Clients { get; }
        IQueryable<Company> Companies { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Priority> Priorities { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<State> States { get; }
  
        #endregion

        #region Methods

        // orders CRUD
        void CreateOrder(Order order);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);        

        // clients CRUD
        void CreateClient(Client client);
        void DeleteClient(Client client);
        void UpdateClient(Client client);
        Task CreateClientAsync(Client client);

        // clients accessors
        IEnumerable<Client> GetClientsForEmployee(string login);

        // companies CRUD
        void CreateCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);

        // employees CRUD
        void CreateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        void UpdateEmployee(Employee employee);

        // existance check methods
        Task<Client> ClientAlreadyExistAsync(string clientName, string companyName);
        Task<Company> CompanyAlreadyExistAsync(string companyName);

        #endregion
    }
}
