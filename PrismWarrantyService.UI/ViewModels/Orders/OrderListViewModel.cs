using System.Collections.ObjectModel;
using System.Threading;
using Prism.Mvvm;
using Prism.Commands;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using System.Linq;
using System.Threading.Tasks;
using PrismWarrantyService.UI.Views.Orders;
using Prism.Events;
using PrismWarrantyService.UI.Events;
using System;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class OrderListViewModel : BindableBase
    {
        #region Fields

        private IRepository repository;
        private IEventAggregator eventAggregator;
        private Order selectedOrder;

        #endregion

        #region Constructors and finalizers

        public OrderListViewModel(IRepository repository, IEventAggregator eventAggregator)
        {
            this.repository = repository;
            this.eventAggregator = eventAggregator;

            Orders = new ObservableCollection<Order>(repository.Orders);

            SelectedOrder = Orders.FirstOrDefault();

            LogoutCommand = new DelegateCommand(Logout);
            AddOrderCommand = new DelegateCommand(AddOrder);
            EditOrderCommand = new DelegateCommand<Order>(EditOrder);
            DeleteOrderCommand = new DelegateCommand<Order>(DeleteOrder);
            OrderSelectionChangedCommand = new DelegateCommand(OrderSelectionChanged);

            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            eventAggregator.GetEvent<OrderAddedEvent>().Subscribe(OrderAddedHandler);
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

        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand AddOrderCommand { get; private set; }
        public DelegateCommand<Order> EditOrderCommand { get; private set; }
        public DelegateCommand<Order> DeleteOrderCommand { get; private set; }
        public DelegateCommand OrderSelectionChangedCommand { get; private set; }

        #endregion

        #region Methods

        private void AddOrder()
        {
            var dialog = new AddOrderView().ShowDialog();
        }

        private async void EditOrder(Order parameter)
        {
            await Task.Factory.StartNew(() => repository.EditOrder(parameter));
        }

        private async void DeleteOrder(Order parameter)
        {
            await Task.Factory.StartNew(() => repository.DeleteOrder(parameter));

            Orders.Remove(parameter);
            SelectedOrder = Orders.FirstOrDefault();
        }

        private void OrderAddedHandler(Order parameter)
        {
            Orders.Add(parameter);
        }

        private void OrderSelectionChanged()
        {
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
        }

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