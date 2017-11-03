using System.Collections.ObjectModel;
using System.Threading;
using Prism.Mvvm;
using Prism.Commands;

using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Services.Authentification.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class OrdersViewModel : BindableBase
    {
        #region Fields
        private IRepository repository;
        private Order selectedOrder;
        private DelegateCommand logoutCommand;
        #endregion

        #region Constructors and finalizers
        public OrdersViewModel(IRepository repo)
        {
            repository = repo;
            Orders = new ObservableCollection<Order>(repository.Orders);

            logoutCommand = new DelegateCommand(Logout);
        }
        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get; set; }

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { SetProperty(ref selectedOrder, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand LogoutCommand { get { return logoutCommand; } }
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