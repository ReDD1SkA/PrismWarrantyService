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
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Events.Lists;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Clients.User
{
    public class UserClientsViewModel : ViewModelBase
    {
        #region Fields

        // Clients fields
        private Client _selectedClient;

        // Sort-filter fields
        private string _filterText;
        private SortPropertyViewModel _sortProperty = new SortPropertyViewModel { Name = "ID", Property = "ClientID" };
        private SortDirectionViewModel _sortDirection = new SortDirectionViewModel { Name = "По возрастанию", Direction = ListSortDirection.Ascending };

        #endregion

        #region Constructors and finalizers

        public UserClientsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Clients properties init
            ClientsSource = new ObservableCollection<Client>(repository
                .Employees
                .First(x => x.Login == Thread.CurrentPrincipal.Identity.Name)
                .Orders
                .Select(x => x.Client)
                .Distinct());
            Clients = new ListCollectionView(ClientsSource);

            SelectedClient = Clients.CurrentItem as Client;

            // Sort-filters properties init
            SortProperties = new[]
            {
                SortProperty,
                new SortPropertyViewModel { Name = "Клиент", Property = "Name" },
                new SortPropertyViewModel { Name = "Компания", Property = "Company.Name" }
            };

            SortDirections = new[]
            {
                SortDirection,
                new SortDirectionViewModel { Name = "По убыванию", Direction = ListSortDirection.Descending }
            };

            Clients.Filter = FilterByName;
            RefreshSort();

            // Commands init
            ClientSelectionChangedCommand = new DelegateCommand(ClientSelectionChanged);

            // Events init
            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
            eventAggregator.GetEvent<NeedRefreshListsEvent>().Subscribe(NeedRefreshListsEventHandler);
            eventAggregator.GetEvent<AuthenticationEvent>().Subscribe(AuthenticationEventHandler);
        }

        #endregion

        #region Properties

        // Clients properties
        public ListCollectionView Clients { get; set; }

        public ObservableCollection<Client> ClientsSource { get; }

        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        // Sort-filter properties
        public IEnumerable<SortPropertyViewModel> SortProperties { get; }

        public IEnumerable<SortDirectionViewModel> SortDirections { get; }

        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); Clients.Refresh(); }
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

        public DelegateCommand ClientSelectionChangedCommand { get; }

        #endregion

        #region Methods

        // Event handlers
        private void ClientSelectionChanged()
        {
            if (SelectedClient != null)
                eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        private void NeedRefreshListsEventHandler()
        {
            ClientsSource.Clear();

            ClientsSource.AddRange(repository
                .Employees
                .First(x => x.Login == Thread.CurrentPrincipal.Identity.Name)
                .Orders
                .Select(x => x.Client)
                .Distinct());
            SelectedClient = Clients.GetItemAt(0) as Client;

            RefreshSort();
        }

        private void AuthenticationEventHandler()
        {
            ClientsSource.Clear();
            ClientsSource.AddRange(repository
                .Employees
                .First(x => x.Login == Thread.CurrentPrincipal.Identity.Name)
                .Orders
                .Select(x => x.Client)
                .Distinct());
        }

        // Sort-filter methods
        private void RefreshSort()
        {
            using (Clients.DeferRefresh())
            {
                Clients.SortDescriptions.Clear();
                Clients.SortDescriptions.Add(new SortDescription(SortProperty.Property, SortDirection.Direction));
            }
        }

        private bool FilterByName(object obj)
        {
            if (!(obj is Client client)) return false;
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            return client.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) != -1
                || client.Company.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        #endregion
    }
}
