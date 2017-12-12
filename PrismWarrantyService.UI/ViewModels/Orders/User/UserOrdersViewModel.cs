using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Authentication;
using PrismWarrantyService.UI.Events.Orders;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Orders.User
{
    public class UserOrdersViewModel : ViewModelBase
    {
        #region Fields

        private Order _selectedOrder;

        #endregion

        #region Constructors and finalizers

        public UserOrdersViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            var orders = repository.Performers
                .Where(x => x.Employee.Login == Thread.CurrentPrincipal.Identity.Name)
                .Select(x => x.Order)
                .OrderBy(x => x.PriorityID);
            Orders = new ObservableCollection<Order>(orders);

            SelectedOrder = Orders.FirstOrDefault();

            OrderSelectionChangedCommand = new DelegateCommand(OrderSelectionChanged);

            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            eventAggregator.GetEvent<AuthenticationEvent>().Subscribe(AuthenticationEventHandler);
        }

        #endregion

        #region Properties

        public ObservableCollection<Order> Orders { get; set; }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value); 
        }

        #endregion

        #region Commands

        public DelegateCommand OrderSelectionChangedCommand { get; }

        #endregion

        #region Methods

        private void OrderSelectionChanged()
        {
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder ?? new Order());
        }

        private void AuthenticationEventHandler()
        {
            var orders = repository.Performers
               .Where(x => x.Employee.Login == Thread.CurrentPrincipal.Identity.Name)
               .Select(x => x.Order)
               .OrderBy(x => x.PriorityID);

            Orders.Clear();
            Orders.AddRange(orders);
            SelectedOrder = Orders.FirstOrDefault();
        }

        #endregion
    }
}