using System.Linq;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.Domain.Abstract
{
    public interface IRepository
    {
        #region Properties
        IQueryable<Client> Clients { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<OrderState> OrderStates { get; }
        IQueryable<OrderType> OrderTypes { get; }
        IQueryable<Remark> Remarks { get; }
        IQueryable<Role> Roles { get; }
        #endregion
    }
}
