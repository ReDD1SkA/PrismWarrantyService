using System.Linq;
using System.Data.Entity;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System;

namespace PrismWarrantyService.Domain.Concrete
{
    public class EFRepository : IRepository
    {
        private WarrantyServiceDbContext context = new WarrantyServiceDbContext();

        #region Properties
        public IQueryable<Client> Clients
        {
            get
            {
                return context.Clients;
            }
        }

        public IQueryable<Department> Departments
        {
            get
            {
                return context.Departments;
            }
        }

        public IQueryable<Employee> Employees
        {
            get
            {
                return context.Employees
                    .Include(x => x.Department)
                    .Include(x => x.Role);
            }
        }

        public IQueryable<Order> Orders
        {
            get
            {
                return context.Orders
                    .Include(x => x.Client)
                    .Include(x => x.OrderState)
                    .Include(x => x.OrderType);
            }
        }

        public IQueryable<OrderState> OrderStates
        {
            get
            {
                return context.OrderStates;
            }
        }

        public IQueryable<OrderType> OrderTypes
        {
            get
            {
                return context.OrderTypes;
            }
        }

        public IQueryable<Remark> Remarks
        {
            get
            {
                return context.Remarks
                    .Include(x => x.Employee)
                    .Include(x => x.Order);
            }
        }

        public IQueryable<Role> Roles
        {
            get
            {
                return context.Roles;
            }
        }
        #endregion

        #region Methods
        public void AddOrder(Order order)
        {
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
            if (order.OrderState.Name.Equals("Выполненный") || order.OrderState.Name.Equals("Отмененный"))
                order.Finished = DateTime.Now;
            else
                order.Finished = null;

            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
        }
        #endregion
    }
}
