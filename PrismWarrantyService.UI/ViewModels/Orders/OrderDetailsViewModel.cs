using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class OrderDetailsViewModel : BindableBase
    {
        #region Fields

        private IRepository repository;
        private IEventAggregator eventAggregator;
        private Order originalSelectedOrder;
        private Order selectedOrder;

        #endregion

        #region Constructors and finalizers

        public OrderDetailsViewModel(IRepository repository, IEventAggregator eventAggregator)
        {
            this.repository = repository;
            this.eventAggregator = eventAggregator;

            OrderStates = new ObservableCollection<OrderState>(repository.OrderStates);
            OrderTypes = new ObservableCollection<OrderType>(repository.OrderTypes);

            EditOrderCommand = new DelegateCommand(EditOrder);

            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Subscribe(OrderSelectionChangedHandler);
        }

        #endregion

        #region Properties

        public Order OriginalSelectedOrder
        {
            get => originalSelectedOrder;
            set => SetProperty(ref originalSelectedOrder, value);
        }

        public Order SelectedOrder
        {
            get => selectedOrder;
            set => SetProperty(ref selectedOrder, value);
        }     

        public ObservableCollection<OrderState> OrderStates { get; set; }

        public ObservableCollection<OrderType> OrderTypes { get; set; }

        #endregion

        #region Commands

        public DelegateCommand EditOrderCommand { get; private set; }

        #endregion

        #region Methods

        private async void EditOrder()
        {
            SelectedOrder.Validate();
            if (SelectedOrder.HasErrors)
                return;

            OriginalSelectedOrder.GetInfoFrom(SelectedOrder);

            await Task.Factory.StartNew(() => repository.EditOrder(OriginalSelectedOrder));
        }

        private void OrderSelectionChangedHandler(Order parameter)
        {
            OriginalSelectedOrder = parameter;
            SelectedOrder = parameter.Clone();
        }

        #endregion
    }
}