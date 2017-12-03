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

        private WarrantyServiceDbContext context = new WarrantyServiceDbContext();

        #endregion

        #region Properties

        public IQueryable<Client> Clients
        {
            get => context.Clients
                .Include(x => x.Company);
        }

        public IQueryable<Company> Companies
        {
            get => context.Companies;
        }

        public IQueryable<Department> Departments
        {
            get => context.Departments;
        }

        public IQueryable<Employee> Employees
        {
            get => context.Employees
                .Include(x => x.Role)
                .Include(x => x.Department);
        }

        public IQueryable<Order> Orders
        {
            get => context.Orders
                .Include(x => x.Client)
                .Include(x => x.Client.Company)
                .Include(x => x.State)
                .Include(x => x.Priority);
        }

        public IQueryable<Performer> Performers
        {
            get => context.Performers
                .Include(x => x.Order)
                .Include(x => x.Employee);
        }

        public IQueryable<Priority> Priorities
        {
            get => context.Priorities;
        }

        public IQueryable<State> States
        {
            get => context.States;
        }

        public IQueryable<Role> Roles
        {
            get => context.Roles;
        }

        #endregion

        #region Methods

        public void AddOrder(Order order)
        {
            order.Accepted = DateTime.Now;

            if (order.State.Name.Equals("Выполненный") || order.State.Name.Equals("Отмененный"))
                order.Finished = DateTime.Now;

            context.Entry(order).State = EntityState.Added;
            context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            context.Entry(order).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void EditOrder(Order order)
        {
            if (order.State.Name.Equals("Выполненный") || order.State.Name.Equals("Отмененный"))
                order.Finished = DateTime.Now;
            else
                order.Finished = null;

            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
        }

        #endregion
    }
}