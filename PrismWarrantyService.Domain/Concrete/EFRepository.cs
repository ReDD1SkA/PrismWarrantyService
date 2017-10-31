using System.Linq;
using System.Data.Entity;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;

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
    }
}
