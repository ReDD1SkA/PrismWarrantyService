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

namespace PrismWarrantyService.UI.ViewModels.Admin.Orders
{
    public class OrdersViewModel : ViewModelBase
    {
        #region Fields

        // Orders fields
        private Order selectedOrder;

        // Sort-filter fields
        private string filterText;
        private SortPropertyViewModel sortProperty = new SortPropertyViewModel() { Name = "ID", Property = "OrderID" };
        private SortDirectionViewModel sortDirection = new SortDirectionViewModel() { Name = "По возрастанию", Direction = ListSortDirection.Ascending };

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
                new SortPropertyViewModel() { Name = "Заказ", Property = "Summary" },
                new SortPropertyViewModel() { Name = "Принят", Property = "Accepted"},
                new SortPropertyViewModel() { Name = "Завершен", Property = "Finished"}
            };

            SortDirections = new[]
            {
                SortDirection,
                new SortDirectionViewModel() { Name = "По убыванию", Direction = ListSortDirection.Descending }
            };

            Orders.Filter = FilterBySummary;
            RefreshSort();

            // Commands init
            AddOrderCommand = new DelegateCommand(AddOrder);
            DeleteOrderCommand = new DelegateCommand<Order>(DeleteOrder);
            OrderSelectionChangedCommand = new DelegateCommand(OrderSelectionChanged);

            // Events init
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            eventAggregator.GetEvent<OrderAddedEvent>().Subscribe(OrderAddedEventHandler);
        }

        #endregion

        #region Properties

        // Orders properties
        public ListCollectionView Orders { get; set; }

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { SetProperty(ref selectedOrder, value); }
        }

        // Sort-filter properties
        public IEnumerable<SortPropertyViewModel> SortProperties { get; private set; }

        public IEnumerable<SortDirectionViewModel> SortDirections { get; private set; }

        public string FilterText
        {
            get { return filterText; }
            set { SetProperty(ref filterText, value); Orders.Refresh(); }
        }

        public SortPropertyViewModel SortProperty
        {
            get { return sortProperty; }
            set { SetProperty(ref sortProperty, value); RefreshSort(); }
        }

        public SortDirectionViewModel SortDirection
        {
            get { return sortDirection; }
            set { SetProperty(ref sortDirection, value); RefreshSort(); }
        }

        #endregion

        #region Commands

        public DelegateCommand AddOrderCommand { get; private set; }
        public DelegateCommand<Order> DeleteOrderCommand { get; private set; }
        public DelegateCommand OrderSelectionChangedCommand { get; private set; }

        #endregion

        #region Methods

        // CRUD methods
        private void AddOrder()
        {
            regionManager.RequestNavigate("Admin.DetailsRegion", "AddOrderView");
        }

        private async void DeleteOrder(Order parameter)
        {
            SelectedOrder = Orders.GetItemAt(0) as Order;
            Orders.Remove(parameter);
            await Task.Run(() => repository.DeleteOrder(parameter));
        }

        // Event handlers
        private void OrderSelectionChanged()
        {
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
        }

        private void OrderAddedEventHandler(Order parameter)
        {
            Orders.AddNewItem(parameter);
            Orders.CommitNew();
            SelectedOrder = parameter;
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
            var order = obj as Order;

            if (order == null) return false;
            if (string.IsNullOrWhiteSpace(FilterText)) return true;;

            //return order.Summary.Contains(FilterText.ToLowerInvariant());
            return order.Summary.StartsWith(FilterText.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion
    }
}