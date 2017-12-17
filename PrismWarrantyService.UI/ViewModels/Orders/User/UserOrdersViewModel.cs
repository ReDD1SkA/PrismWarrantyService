using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Authentication;
using PrismWarrantyService.UI.Events.Lists;
using PrismWarrantyService.UI.Events.Orders;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Orders.User
{
    public class UserOrdersViewModel : ViewModelBase
    {
        #region Fields

        // Orders fields
        private Order _selectedOrder;

        // Sort-filter fields
        private string _filterText;
        private SortPropertyViewModel _sortProperty = new SortPropertyViewModel { Name = "ID", Property = "OrderID" };
        private SortDirectionViewModel _sortDirection = new SortDirectionViewModel { Name = "По возрастанию", Direction = ListSortDirection.Ascending };

        #endregion

        #region Constructors and finalizers

        public UserOrdersViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Orders properties init

            OrdersSource = new ObservableCollection<Order>(repository
                .Employees
                .First(x => x.Login == Thread.CurrentPrincipal.Identity.Name)
                .Orders);
            Orders = new ListCollectionView(OrdersSource);

            SelectedOrder = Orders.CurrentItem as Order;

            // Sort-filters properties init
            SortProperties = new[]
            {
                SortProperty,
                new SortPropertyViewModel { Name = "Заказ", Property = "Summary" },
                new SortPropertyViewModel { Name = "Принят", Property = "Accepted"},
                new SortPropertyViewModel { Name = "Завершен", Property = "Finished"},
                new SortPropertyViewModel { Name = "Приоритет", Property = "PriorityID"},
                new SortPropertyViewModel { Name = "Статус", Property = "StateID"}
            };

            SortDirections = new[]
            {
                SortDirection,
                new SortDirectionViewModel { Name = "По убыванию", Direction = ListSortDirection.Descending }
            };

            Orders.Filter = FilterBySummary;
            RefreshSort();

            // Commands init
            OrderSelectionChangedCommand = new DelegateCommand(OrderSelectionChanged);

            // Events init
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            eventAggregator.GetEvent<NeedRefreshListsEvent>().Subscribe(NeedRefreshListsEventHandler);
            eventAggregator.GetEvent<AuthenticationEvent>().Subscribe(AuthenticationEventHandler);
        }

        #endregion

        #region Properties

        // Orders properties
        public ListCollectionView Orders { get; set; }

        public ObservableCollection<Order> OrdersSource { get; }

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

        public DelegateCommand OrderSelectionChangedCommand { get; }

        #endregion

        #region Methods

        // Event handlers
        private void OrderSelectionChanged()
        {
            if (SelectedOrder != null) eventAggregator.GetEvent<OrderSelectionChangedEvent>().Publish(SelectedOrder);
            regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
        }

        private void NeedRefreshListsEventHandler()
        {
            OrdersSource.Clear();
            OrdersSource.AddRange(repository.Orders);

            SelectedOrder = Orders.GetItemAt(0) as Order;

            RefreshSort();
        }

        private void AuthenticationEventHandler()
        {
            OrdersSource.Clear();
            OrdersSource.AddRange(repository
                .Employees
                .First(x => x.Login == Thread.CurrentPrincipal.Identity.Name)
                .Orders);
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
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            return order.Summary.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) != -1
                || order.Client.Name.IndexOf(FilterText, StringComparison.CurrentCultureIgnoreCase) != -1
                || order.Client.Company.Name.IndexOf(FilterText, StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        #endregion
    }
}