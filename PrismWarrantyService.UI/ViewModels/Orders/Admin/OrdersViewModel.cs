using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Orders;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Orders.Admin
{
    public class OrdersViewModel : ViewModelBase
    {
        #region Fields

        // Orders fields
        private Order _selectedOrder;

        // Sort-filter fields
        private string _filterText;
        private SortPropertyViewModel _sortProperty = new SortPropertyViewModel() { Name = "ID", Property = "OrderID" };
        private SortDirectionViewModel _sortDirection = new SortDirectionViewModel() { Name = "По возрастанию", Direction = ListSortDirection.Ascending };

        #endregion

        #region Constructors and finalizers

        public OrdersViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Orders properties init
            Orders = new ListCollectionView(repository.Orders.ToList());
            SelectedOrder = Orders.CurrentItem as Order;

            // Sort-filters properties init
            SortProperties = new[]
            {
                SortProperty,
                new SortPropertyViewModel { Name = "Заказ", Property = "Summary" },
                new SortPropertyViewModel { Name = "Принят", Property = "Accepted"},
                new SortPropertyViewModel { Name = "Завершен", Property = "Finished"}
            };

            SortDirections = new[]
            {
                SortDirection,
                new SortDirectionViewModel { Name = "По убыванию", Direction = ListSortDirection.Descending }
            };

            Orders.Filter = FilterBySummary;
            RefreshSort();

            // Commands init
            CreateOrderCommand = new DelegateCommand(CreateOrder);
            OrderSelectionChangedCommand = new DelegateCommand(OrderSelectionChanged);

            // Events init
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            eventAggregator.GetEvent<OrderCreatedEvent>().Subscribe(OrderCreatedEventHandler);
            eventAggregator.GetEvent<OrderDeletedEvent>().Subscribe(OrderDeletedEventHandler);
        }

        #endregion

        #region Properties

        // Orders properties
        public ListCollectionView Orders { get; set; }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        // Sort-filter properties
        public IEnumerable<SortPropertyViewModel> SortProperties { get; }

        public IEnumerable<SortDirectionViewModel> SortDirections { get; }

        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); Orders.Refresh(); }
        }

        public SortPropertyViewModel SortProperty
        {
            get { return _sortProperty; }
            set { SetProperty(ref _sortProperty, value); RefreshSort(); }
        }

        public SortDirectionViewModel SortDirection
        {
            get { return _sortDirection; }
            set { SetProperty(ref _sortDirection, value); RefreshSort(); }
        }

        #endregion

        #region Commands

        public DelegateCommand CreateOrderCommand { get; }
        public DelegateCommand OrderSelectionChangedCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private void CreateOrder()
        {
            regionManager.RequestNavigate("Admin.DetailsRegion", "CreateOrderView");
        }

        // Event handlers
        private void OrderSelectionChanged()
        {
            if (SelectedOrder != null) eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
        }

        private void OrderCreatedEventHandler(Order parameter)
        {
            Orders.AddNewItem(parameter);
            Orders.CommitNew();
            SelectedOrder = parameter;
        }

        private void OrderDeletedEventHandler(Order parameter)
        {
            Orders.Remove(parameter);
            SelectedOrder = Orders.GetItemAt(0) as Order;
        }

        // Sort-filter methods
        private void RefreshSort()
        {
            using (Orders.DeferRefresh())
            {
                Orders.SortDescriptions.Clear();
                Orders.SortDescriptions.Add(new SortDescription(SortProperty.Property, SortDirection.Direction));
            }
        }

        private bool FilterBySummary(object obj)
        {
            if (!(obj is Order order)) return false;
            if (string.IsNullOrWhiteSpace(FilterText)) return true;;

            return order.Summary.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        #endregion
    }
}