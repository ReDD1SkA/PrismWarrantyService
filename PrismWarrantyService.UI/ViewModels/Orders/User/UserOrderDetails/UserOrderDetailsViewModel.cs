using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Orders;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Orders.User.UserOrderDetails
{
    public class UserOrderDetailsViewModel : ViewModelBase
    {
        #region Fields

        private Order _selectedOrder;
        private bool _canBeAccepted;
        private bool _canBeCompleted;

        #endregion

        #region Constructors and finalizers

        public UserOrderDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Сommands init
            OrderAcceptedCommand = new DelegateCommand(OrderAccepted);
            OrderCompletedCommand = new DelegateCommand(OrderCompleted);

            // Events init
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Subscribe(OrderSelectionChangedEventHandler);
        }

        #endregion

        #region Properties

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        public bool CanBeAccepted
        {
            get => _canBeAccepted;
            set => SetProperty(ref _canBeAccepted, value);
        }

        public bool CanBeCompleted
        {
            get => _canBeCompleted;
            set => SetProperty(ref _canBeCompleted, value);
        }

        #endregion

        #region Commands

        public DelegateCommand OrderAcceptedCommand { get; }
        public DelegateCommand OrderCompletedCommand { get; }

        #endregion

        #region Methods

        private async void OrderAccepted()
        {
            SelectedOrder.State = repository.States
                .FirstOrDefault(x => x.Name == "Выполняемый");

            await Task.Run(() => repository.UpdateOrder(SelectedOrder));

            CanBeAccepted = false;
            CanBeCompleted = true;
        }

        private async void OrderCompleted()
        {
            SelectedOrder.State = repository.States
                .FirstOrDefault(x => x.Name == "Выполненный");

            await Task.Run(() => repository.UpdateOrder(SelectedOrder));

            CanBeAccepted = false;
            CanBeCompleted = false;
        }

        private void OrderSelectionChangedEventHandler(Order parameter)
        {
            SelectedOrder = parameter;

            CanBeAccepted = SelectedOrder.State.Name == "Новый";
            CanBeCompleted = SelectedOrder.State.Name == "Выполняемый";  
        }

        #endregion
    }
}