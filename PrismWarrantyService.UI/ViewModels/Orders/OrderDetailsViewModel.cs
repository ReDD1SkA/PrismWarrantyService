using Prism.Events;
using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class OrderDetailsViewModel : BindableBase
    {
        #region Fields

        private IRepository repository;
        private IEventAggregator eventAggregator;
        private Order selectedOrder;

        #endregion

        #region Constructors and finalizers

        public OrderDetailsViewModel(IRepository repository, IEventAggregator eventAggregator)
        {
            this.repository = repository;
            this.eventAggregator = eventAggregator;

            OrderStates = new ObservableCollection<OrderState>(repository.OrderStates);
            OrderTypes = new ObservableCollection<OrderType>(repository.OrderTypes);

            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Subscribe(OrderSelectionChangedHandler);
        }

        #endregion

        #region Properties

        public Order SelectedOrder
        {
            get => selectedOrder;
            set => SetProperty(ref selectedOrder, value);
        }

        public ObservableCollection<OrderState> OrderStates { get; set; }

        public ObservableCollection<OrderType> OrderTypes { get; set; }

        #endregion

        #region Methods

        private void OrderSelectionChangedHandler(Order parameter)
        {
            SelectedOrder = parameter;
        }

        #endregion
    }
}