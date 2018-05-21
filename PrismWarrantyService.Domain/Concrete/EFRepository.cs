using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.Domain.Concrete
{
    public class EFRepository : IRepository
    {
        #region Fields

        private readonly WarrantyServiceDbContext _context = new WarrantyServiceDbContext();

        #endregion

        #region Client methods

        public void CreateClient(Client client)
        {
            _context.Entry(client).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteClient(Client client)
        {
            var clientOrders = _context.Orders
                .Where(x => x.ClientID == client.ClientID);

            foreach (var order in clientOrders)
            {
                _context.Entry(order).State = EntityState.Deleted;
            }

            _context.Entry(client).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public async Task CreateClientAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients
                .ToList();
        }

        public IEnumerable<Client> GetClientsForEmployee(string login)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients
                .ToListAsync();
        }

        public async Task<Client> ClientAlreadyExistAsync(string clientName)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(x => x.Title == clientName);
        }

        #endregion

        #region Device methods

        public void CreateDevice(Device device)
        {
            _context.Entry(device).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void UpdateDevice(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteDevice(Device device)
        {
            _context.Entry(device).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<Device> GetAllDevices()
        {
            return _context.Devices
                .Include(x => x.Producer)
                .Include(x => x.Producer.Country)
                .ToList();
        }

        #endregion

        #region Employee methods

        public async Task CreateEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees
                .Include(x => x.Room)
                .Include(x => x.Position)
                .Include(x => x.Role)
                .ToList();
        }

        public Employee GetEmployeeByLogin(string login)
        {
            return _context.Employees
                .FirstOrDefault(x => x.Login == login);
        }

        public async Task<Employee> GetEmployeeByLoginAsync(string login)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Login == login);
        }

        #endregion

        #region Order methods

        public void CreateOrder(Order order)
        {
            order.Accepted = DateTime.Now;

            if (order.State.Name.Equals("Выполненный") || order.State.Name.Equals("Отмененный"))
                order.Finished = DateTime.Now;

            _context.Entry(order).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            if (order.State.Name.Equals("Выполненный"))
                order.Finished = DateTime.Now;
            else
                order.Finished = null;

            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(x => x.Client)
                .Include(x => x.Device)
                .Include(x => x.Priority)
                .Include(x => x.State)
                .ToList();
        }

        public IEnumerable<Order> GetOrdersByClientID(int clientID)
        {
            return _context.Orders
                .Where(x => x.ClientID == clientID)
                .ToList();
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIDAsync(int clientID)
        {
            return await _context.Orders
                .Where(x => x.ClientID == clientID)
                .ToListAsync();
        }

        #endregion

        #region Position methods

        public IEnumerable<Position> GetAllPositions()
        {
            return _context.Positions
                .ToList();
        }

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return await _context.Positions
                .ToListAsync();
        }

        #endregion

        #region Priority methods

        public IEnumerable<Priority> GetAllPriorities()
        {
            return _context.Priorities
                .ToList();
        }

        public async Task<IEnumerable<Priority>> GetAllPrioritiesAsync()
        {
            return await _context.Priorities
                .ToListAsync();
        }

        #endregion

        #region Role methods

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles
                .ToList();
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles
                .ToListAsync();
        }

        #endregion

        #region Room methods

        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms
                .ToList();
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms
                .ToListAsync();
        }

        #endregion

        #region State methods

        public IEnumerable<State> GetAllStates()
        {
            return _context.States
                .ToList();
        }

        public async Task<IEnumerable<State>> GetAllStatesAsync()
        {
            return await _context.States
                .ToListAsync();
        }

        #endregion
    }
}