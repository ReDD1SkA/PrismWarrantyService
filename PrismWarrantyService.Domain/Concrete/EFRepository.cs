using System.Linq;
using System.Data.Entity;
using PrismWarrantyService.Domain.Abstract;
using System;
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
            get => context.Clients;
        }

        public IQueryable<Department> Departments
        {
            get => context.Departments;
        }

        public IQueryable<Employee> Employees
        {
            get => context.Employees
                    .Include(x => x.Department)
                    .Include(x => x.Role);
        }

        public IQueryable<Order> Orders
        {
            get => context.Orders
                    .Include(x => x.Client)
                    .Include(x => x.Employee)
                    .Include(x => x.OrderState)
                    .Include(x => x.OrderType);
        }

        public IQueryable<OrderState> OrderStates
        {
            get => context.OrderStates;
        }

        public IQueryable<OrderType> OrderTypes
        {
            get => context.OrderTypes;
        }

        public IQueryable<Remark> Remarks
        {
            get => context.Remarks
                    .Include(x => x.Employee)
                    .Include(x => x.Order);
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

            // TODO:
            // поставить заказ в очередь с учетом срочности
            // если он не является выполненным/отмененным

            if (order.OrderState.Name.Equals("Выполненный") || order.OrderState.Name.Equals("Отмененный"))
                order.Finished = DateTime.Now;

            context.Entry(order).State = EntityState.Added;
            context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            // TODO:
            // удалить заказ из очереди

            context.Entry(order).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void EditOrder(Order order)
        {
            // TODO:
            // изменить позицию заказа в зависимости от его типа и статуса

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