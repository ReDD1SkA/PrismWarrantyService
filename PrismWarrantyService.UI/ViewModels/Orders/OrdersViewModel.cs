using System.Collections.ObjectModel;
using System.Threading;
using Prism.Mvvm;
using Prism.Commands;

using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using System.Linq;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class OrdersViewModel : BindableBase
    {
        #region Fields
        private IRepository repository;
        private Order selectedOrder;
        private DelegateCommand logoutCommand;
        private DelegateCommand addOrderCommand;
        private DelegateCommand<Order> editOrderCommand;
        private DelegateCommand<Order> deleteOrderCommand;
        #endregion

        #region Constructors and finalizers
        public OrdersViewModel(IRepository repo)
        {
            repository = repo;

            Orders = new ObservableCollection<Order>(repository.Orders);
            OrderStates = new ObservableCollection<OrderState>(repository.OrderStates);
            OrderTypes = new ObservableCollection<OrderType>(repository.OrderTypes);

            SelectedOrder = Orders.FirstOrDefault();

            logoutCommand = new DelegateCommand(Logout);
            editOrderCommand = new DelegateCommand<Order>(repository.EditOrder);
            deleteOrderCommand = new DelegateCommand<Order>(repository.DeleteOrder);
        }
        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<OrderState> OrderStates { get; set; }
        public ObservableCollection<OrderType> OrderTypes { get; set; }

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { SetProperty(ref selectedOrder, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand LogoutCommand { get { return logoutCommand; } }
        public DelegateCommand AddOrderCommand { get { return addOrderCommand; } }
        public DelegateCommand<Order> EditOrderCommand { get { return editOrderCommand; } }
        public DelegateCommand<Order> DeleteOrderCommand { get { return deleteOrderCommand; } }
        #endregion

        #region Methods
        private void Logout()
        {
            if (Thread.CurrentPrincipal is CustomPrincipal customPrincipal)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                
                // ERROR
            }
        }
        #endregion
    }
}