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

        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);

        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetClientsForEmployee(string login);    

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

        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);

        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeByLogin(string login);

        Task<Employee> GetEmployeeByLoginAsync(string login);

        #endregion

        #region Order methods

        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByClientID(int ClientID);

        Task<IEnumerable<Order>> GetOrdersByClientIDAsync(int clientID);

        #endregion

        #region Position methods

        IEnumerable<Position> GetAllPositions();
        Task<IEnumerable<Position>> GetAllPositionsAsync();

        #endregion

        #region Priority methods

        IEnumerable<Priority> GetAllPriorities();
        Task<IEnumerable<Priority>> GetAllPrioritiesAsync();

        #endregion

        #region Roles

        IEnumerable<Role> GetAllRoles();
        Task<IEnumerable<Role>> GetAllRolesAsync();

        #endregion

        #region Room methods

        IEnumerable<Room> GetAllRooms();
        Task<IEnumerable<Room>> GetAllRoomsAsync();

        #endregion

        #region State methods

        IEnumerable<State> GetAllStates();
        Task<IEnumerable<State>> GetAllStatesAsync();

        #endregion
    }
}
