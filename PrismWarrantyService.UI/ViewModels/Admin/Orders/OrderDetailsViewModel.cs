using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismWarrantyService.UI.ViewModels.Admin.Orders
{
    public class OrderDetailsViewModel : ViewModelBase
    {
        #region Fields

        private Order originalSelectedOrder;
        private Order selectedOrder;

        #endregion

        #region Constructors and finalizers

        public OrderDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            States = new ObservableCollection<State>(repository.States);
            Priorities = new ObservableCollection<Priority>(repository.Priorities);

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

        public ObservableCollection<State> States { get; set; }

        public ObservableCollection<Priority> Priorities { get; set; }

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