﻿using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Commands;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using PrismWarrantyService.UI.Events;
using Prism.Regions;

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
        public DelegateCommand<Order> DeleteOrderCommand { get; private set; }
        public DelegateCommand OrderSelectionChangedCommand { get; private set; }

        #endregion

        #region Methods

        private void AddOrder()
        {
            regionManager.RequestNavigate("DetailsRegion", "AddOrderView");
        }

        private async void DeleteOrder(Order parameter)
        {
            SelectedOrder = Orders.FirstOrDefault();
            Orders.Remove(parameter);

            await Task.Factory.StartNew(() => repository.DeleteOrder(parameter));
        }

        private void OrderAddedHandler(Order parameter)
        {
            Orders.Add(parameter);
            SelectedOrder = Orders.LastOrDefault();
        }

        private void OrderSelectionChanged()
        {
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
        }

        #endregion
    }
}