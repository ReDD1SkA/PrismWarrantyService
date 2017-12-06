using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Orders;

namespace PrismWarrantyService.UI.ViewModels.Admin.Orders
{
    public class OrdersViewModel : ViewModelBase
    {
        #region Fields

        private Order selectedOrder;

        #endregion

        #region Constructors and finalizers

        public OrdersViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            Orders = new ObservableCollection<Order>(repository.Orders);
            SelectedOrder = Orders.FirstOrDefault();

            AddOrderCommand = new DelegateCommand(AddOrder);
            DeleteOrderCommand = new DelegateCommand<Order>(DeleteOrder);
            OrderSelectionChangedCommand = new DelegateCommand(OrderSelectionChanged);

            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            eventAggregator.GetEvent<OrderAddedEvent>().Subscribe(OrderAddedEventHandler);
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

        public DelegateCommand AddOrderCommand { get; private set; }
        public DelegateCommand<Order> DeleteOrderCommand { get; private set; }
        public DelegateCommand OrderSelectionChangedCommand { get; private set; }

        #endregion

        #region Methods

        private void AddOrder()
        {
            regionManager.RequestNavigate("Admin.DetailsRegion", "AddOrderView");
        }

        private async void DeleteOrder(Order parameter)
        {
            SelectedOrder = Orders.FirstOrDefault();
            Orders.Remove(parameter);

            await Task.Run(() => repository.DeleteOrder(parameter));
        }

        private void OrderSelectionChanged()
        {
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
        }

        private void OrderAddedEventHandler(Order parameter)
        {
            Orders.Add(parameter);
            SelectedOrder = Orders.LastOrDefault();
        }

        #endregion
    }
}