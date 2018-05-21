using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Events.Orders;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Clients.Admin
{
    public class ClientsViewModel : ViewModelBase
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

        public ClientsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            // Clients properties init
            ClientsSource = new ObservableCollection<Client>(repo.GetAllClients());
            Clients = new ListCollectionView(ClientsSource);

            SelectedClient = Clients.CurrentItem as Client;
            CheckedClients = new List<Client>();

            // Client orders init
            if (SelectedClient != null)
                ClientOrders = new ObservableCollection<Order>(repo.GetOrdersByClientID(SelectedClient.ClientID));

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
            CreateClientCommand = new DelegateCommand(CreateClient);
            DeleteClientCommand = new DelegateCommand(DeleteClient);
            ClientSelectionChangedCommand = new DelegateCommand(ClientSelectionChanged);
            ClientCheckedCommand = new DelegateCommand<Client>(ClientChecked);
            ClientUncheckedCommand = new DelegateCommand<Client>(ClientUnchecked);

            // Events init
            eventAggregator.GetEvent<OrderListChangedEvent>().Subscribe(OrderListChangedEventHandler, ThreadOption.UIThread);
            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
        }

        #endregion

        #region Properties

        // Clients properties
        public ListCollectionView Clients { get; set; }

        public ObservableCollection<Client> ClientsSource { get; }

        public List<Client> CheckedClients { get; set; }

        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        // Client order properties
        public ObservableCollection<Order> ClientOrders { get; }

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

        public DelegateCommand CreateClientCommand { get; }
        public DelegateCommand DeleteClientCommand { get; }
        public DelegateCommand ClientSelectionChangedCommand { get; }
        public DelegateCommand<Client> ClientCheckedCommand { get; }
        public DelegateCommand<Client> ClientUncheckedCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private void CreateClient()
        {
            regionManager.RequestNavigate("Admin.DetailsRegion", "CreateClientView");
        }

        private async void DeleteClient()
        {
            if (CheckedClients.Count == 0)
                return;

            foreach (var client in CheckedClients)
            {
                ClientOrders.Clear();               
                await repo.DeleteClientAsync(client);
            }
            CheckedClients.Clear();

            ClientListChangedEventHandler();
            OrderListChangedEventHandler();
        }

        private void ClientChecked(Client parameter)
        {
            CheckedClients.Add(parameter);
        }

        private void ClientUnchecked(Client parameter)
        {
            CheckedClients.Remove(parameter);
        }

        // Event handlers
        private async void ClientSelectionChanged()
        {
            ClientOrders.Clear();

            if (SelectedClient != null)
                ClientOrders.AddRange(await repo.GetOrdersByClientIDAsync(SelectedClient.ClientID));

            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        private async void ClientListChangedEventHandler()
        {
            ClientsSource.Clear();
            ClientOrders.Clear();
            ClientsSource.AddRange(await repo.GetAllClientsAsync());

            if (ClientsSource.Any())
            {
                CheckedClients = CheckedClients.Where(x => ClientsSource.Contains(x)).ToList();
                SelectedClient = Clients.GetItemAt(0) as Client;
            }
            else
            {
                CheckedClients.Clear();
                SelectedClient = null;
            }

            OrderListChangedEventHandler();
            RefreshSort();
        }

        private async void OrderListChangedEventHandler()
        {
            ClientOrders.Clear();
            if (SelectedClient != null)
                ClientOrders.AddRange(await repo.GetOrdersByClientIDAsync(SelectedClient.ClientID));
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

            return client.Title.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        #endregion
    }
}