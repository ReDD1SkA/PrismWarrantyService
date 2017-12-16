using System;
using System.Linq;
using System.Data.Entity;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.Domain.Concrete
{
    public class EFRepository : IRepository
    {
        #region Fields

        private readonly WarrantyServiceDbContext _context = new WarrantyServiceDbContext();

        #endregion

        #region Properties

        public IQueryable<Client> Clients => _context.Clients
            .Include(x => x.Company);

        public IQueryable<Company> Companies => _context.Companies;

        public IQueryable<Employee> Employees => _context.Employees
            .Include(x => x.Role);

        public IQueryable<Order> Orders => _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Client.Company)
            .Include(x => x.State)
            .Include(x => x.Priority);

        public IQueryable<Priority> Priorities => _context.Priorities;

        public IQueryable<State> States => _context.States;

        public IQueryable<Role> Roles => _context.Roles;

        #endregion

        #region Methods

        // Orders CRUD
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
            if (order.State.Name.Equals("Выполненный") || order.State.Name.Equals("Отмененный"))
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

        // Clients CRUD
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

        // Companies CRUD
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

        // Employees CRUD
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
            // могут опять не работать FK в таблице Performers

            _context.Entry(employee).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        #endregion
    }
}