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

        public IEnumerable<Client> GetAllCliens()
        {
            return _context.Clients
                .ToList();
        }

        public IEnumerable<Client> GetClientsForEmployee(Employee employee)
        {
            return _context.Orders
                .Include(x => x.Client)
                .Where(x => x.Employees.Contains(employee))
                .Select(x => x.Client)
                .Distinct();
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients
                .ToListAsync();
        }
 
        public async Task CreateClientAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<Client> ClientAlreadyExistAsync(string clientName, string companyName)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(x => x.Title == clientName);
        }

        #endregion

        #region Methods

        // orders CRUD
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

        // clients CRUD
        

        // companies CRUD
        public void CreateCompany(Company company)
        {
            _context.Entry(company).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void UpdateCompany(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCompany(Company company)
        {
            var companyClients = _context.Clients
                .Where(x => x.CompanyID == company.CompanyID);

            foreach (var client in companyClients)
            {
                var clientOrders = _context.Orders
                    .Where(x => x.ClientID == client.ClientID);

                foreach (var order in clientOrders)
                {
                    _context.Entry(order).State = EntityState.Deleted;
                }

                _context.Entry(client).State = EntityState.Deleted;
            }
            _context.Entry(company).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        // employees CRUD
        public void CreateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        // existance check methods
        

        public async Task<Company> CompanyAlreadyExistAsync(string companyName)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(x => x.Name == companyName);
        }

        // clients accessors
        public IEnumerable<Client> GetClientsForEmployee(string login)
        {
            var clients = _context.Employees
                .Include(x => x.Orders)
                .First(x => x.Login == login)
                .Orders
                .Select(x => x.ClientID);

            return _context.Clients
                .Where(x => clients.Contains(x.ClientID));
        }

        #endregion
    }
}