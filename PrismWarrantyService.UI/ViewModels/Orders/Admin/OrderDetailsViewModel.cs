using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Orders;

namespace PrismWarrantyService.UI.ViewModels.Orders.Admin
{
    public class OrderDetailsViewModel : ViewModelBase
    {
        #region Fields

        private Order _originalSelectedOrder;
        private Order _selectedOrder;

        #endregion

        #region Constructors and finalizers

        public OrderDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Subscribe(OrderSelectionChangedEventHandler);

            EditOrderCommand = new DelegateCommand(EditOrder);

            States = new ObservableCollection<State>(repository.States);
            Priorities = new ObservableCollection<Priority>(repository.Priorities);

            OriginalSelectedOrder = repository.Orders
                .FirstOrDefault();
            SelectedOrder = OriginalSelectedOrder.Clone();
        }

        #endregion

        #region Properties

        public Order OriginalSelectedOrder
        {
            get => _originalSelectedOrder;
            set => SetProperty(ref _originalSelectedOrder, value);
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }     

        public ObservableCollection<State> States { get; set; }

        public ObservableCollection<Priority> Priorities { get; set; }

        #endregion

        #region Commands

        public DelegateCommand EditOrderCommand { get; }

        #endregion

        #region Methods

        private async void EditOrder()
        {
            SelectedOrder.Validate();
            if (SelectedOrder.HasErrors)
                return;

            OriginalSelectedOrder.GetInfoFrom(SelectedOrder);

            await Task.Run(() => repository.UpdateOrder(OriginalSelectedOrder));
        }

        private void OrderSelectionChangedEventHandler(Order parameter)
        {
            OriginalSelectedOrder = parameter;
            SelectedOrder = parameter.Clone();
        }

        #endregion
    }
}