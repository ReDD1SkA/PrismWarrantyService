using PrismWarrantyService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismWarrantyService.Domain.Abstract
{
    public interface IRepository
    {
        #region Client methods

        void CreateClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);

        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetClientsForEmployee(string login);

        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task CreateClientAsync(Client client);
        Task<Client> ClientAlreadyExistAsync(string clientName, string companyName);

        #endregion

        #region Device methods

        void CreateDevice(Device device);
        void UpdateDevice(Device device);
        void DeleteDevice(Device device);

        IEnumerable<Device> GetAllDevices();

        #endregion

        #region Employee methods

        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

        IEnumerable<Employee> GetAllEmployees();

        #endregion

        #region Order methods

        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);

        IEnumerable<Order> GetAllOrders();

        #endregion

    }
}
