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

        public IQueryable<Client> Clients
        {
            get => _context.Clients
                .Include(x => x.Company);
        }

        public IQueryable<Company> Companies
        {
            get => _context.Companies;
        }

        public IQueryable<Employee> Employees
        {
            get => _context.Employees
                .Include(x => x.Role);
        }

        public IQueryable<Order> Orders
        {
            get => _context.Orders
                .Include(x => x.Client)
                .Include(x => x.Client.Company)
                .Include(x => x.State)
                .Include(x => x.Priority);
        }

        public IQueryable<Performer> Performers
        {
            get => _context.Performers
                .Include(x => x.Order)
                .Include(x => x.Employee);
        }

        public IQueryable<Priority> Priorities
        {
            get => _context.Priorities;
        }

        public IQueryable<State> States
        {
            get => _context.States;
        }

        public IQueryable<Role> Roles
        {
            get => _context.Roles;
        }

        #endregion

        #region Methods

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

        #endregion
    }
}