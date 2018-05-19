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

        Task CreateClientAsync(Client client);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> ClientAlreadyExistAsync(string clientName);

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

        #region Priority methods

        IEnumerable<Priority> GetAllPriorities();
        Task<IEnumerable<Priority>> GetAllPrioritiesAsync();

        #endregion

        #region State methods

        IEnumerable<State> GetAllStates();
        Task<IEnumerable<State>> GetAllStatesAsync();

        #endregion
    }
}
