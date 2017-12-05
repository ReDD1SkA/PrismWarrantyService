using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;

namespace PrismWarrantyService.UI.ViewModels.User.Orders
{
    public class UserOrdersViewModel : ViewModelBase
    {
        #region Fields

        private Order selectedOrder;

        #endregion

        #region Constructors and finalizers

        public UserOrdersViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            var orders = repository.Performers
                .Where(x => x.Employee.Login == Thread.CurrentPrincipal.Identity.Name)
                .Select(x => x.Order);
            Orders = new ObservableCollection<Order>(orders);

            SelectedOrder = Orders.FirstOrDefault();

            OrderSelectionChangedCommand = new DelegateCommand(OrderSelectionChanged);

            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            eventAggregator.GetEvent<AuthenticationEvent>().Subscribe(AuthenticationHandler);
        }

        #endregion

        #region Properties

        public ObservableCollection<Order> Orders { get; set; }

        public Order SelectedOrder
        {
            get => selectedOrder;
            set => SetProperty(ref selectedOrder, value); 
        }

        #endregion

        #region Commands

        public DelegateCommand OrderSelectionChangedCommand { get; private set; }

        #endregion

        #region Methods

        private void OrderSelectionChanged()
        {
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder ?? new Order());
        }

        private void AuthenticationHandler()
        {
            var orders = repository.Performers
               .Where(x => x.Employee.Login == Thread.CurrentPrincipal.Identity.Name)
               .Select(x => x.Order);

            Orders.Clear();
            Orders.AddRange(orders);
            SelectedOrder = Orders.FirstOrDefault();
        }

        #endregion
    }
}