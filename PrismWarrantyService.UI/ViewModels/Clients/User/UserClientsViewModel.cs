﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Authentication;
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Services.ViewModels;

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

        public UserClientsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            // Clients properties init
            ClientsSource = new ObservableCollection<Client>(repo.GetClientsForEmployee(Thread.CurrentPrincipal.Identity.Name));
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
            eventAggregator.GetEvent<AuthenticationEvent>().Subscribe(AuthenticationEventHandler, ThreadOption.UIThread);
            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
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
            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        private void CompanyListChangedEventHandler()
        {
            ClientsSource.Clear();
            ClientsSource.AddRange(repo.GetClientsForEmployee(Thread.CurrentPrincipal.Identity.Name));

            try
            {
                SelectedClient = Clients.GetItemAt(0) as Client;
            }
            catch (ArgumentOutOfRangeException) { }

            RefreshSort();
        }

        private void AuthenticationEventHandler()
        {
            ClientsSource.Clear();
            ClientsSource.AddRange(repo.GetClientsForEmployee(Thread.CurrentPrincipal.Identity.Name));
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
