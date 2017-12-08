using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Events.Companies;
using PrismWarrantyService.UI.Events.Orders;

namespace PrismWarrantyService.UI.ViewModels.Orders.Admin.CreateOrder
{
    public class CreateOrderViewModel : ViewModelBase
    {
        #region Fields

        private Order _newOrder;
        private Client _newClient;
        private Company _newCompany;

        #endregion

        #region Constructors and finalizers

        public CreateOrderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            Clients = new ObservableCollection<Client>(repository.Clients);
            Priorities = new ObservableCollection<Priority>(repository.Priorities);
            States = new ObservableCollection<State>(repository.States);
            Companies = new ObservableCollection<Company>(repository.Companies);

            NewOrder = new Order { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
            NewClient = new Client { Company = Companies.FirstOrDefault() };
            NewCompany = new Company();

            SaveCommand = new DelegateCommand(SaveOrder);
            CancelCommand = new DelegateCommand(Cancel);
            SelectOrderClientCommand = new DelegateCommand(SelectOrderClient);
            AddNewClientToOrderCommand = new DelegateCommand(AddNewClientToOrder);
            SelectClientCompanyCommand = new DelegateCommand(SelectClientCompany);
            AddNewCompanyToClientCommand = new DelegateCommand(AddNewCompanyToClient);
        }

        #endregion

        #region Properties

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<Priority> Priorities { get; set; }
        public ObservableCollection<State> States { get; set; }

        public bool NeedNewClient { get; private set; }
        public bool NeedNewCompany { get; private set; }

        public Order NewOrder
        {
            get => _newOrder;
            set => SetProperty(ref _newOrder, value);
        }

        public Client NewClient
        {
            get => _newClient;
            set => SetProperty(ref _newClient, value);
        }

        public Company NewCompany
        {
            get => _newCompany;
            set => SetProperty(ref _newCompany, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand SelectOrderClientCommand { get; }
        public DelegateCommand AddNewClientToOrderCommand { get; }
        public DelegateCommand SelectClientCompanyCommand { get; }
        public DelegateCommand AddNewCompanyToClientCommand { get; }

        #endregion

        #region Methods

        private void SelectOrderClient()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectOrderClientRegion", "CreateOrderSelectClientView");
            NeedNewClient = false;
        }

        private void AddNewClientToOrder()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectOrderClientRegion", "CreateOrderNewClientView");
            NeedNewClient = true;
        }

        private void SelectClientCompany()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectClientCompanyRegion", "CreateOrderSelectCompanyView");
            NeedNewCompany = false;
        }

        private void AddNewCompanyToClient()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectClientCompanyRegion", "CreateOrderNewCompanyView");
            NeedNewCompany = true;
        }

        private void SaveOrder()
        {
            NewOrder.Validate();
            if (NewOrder.IsValid)
            {
                if (NeedNewClient)
                {
                    NewClient.Validate();
                    if (NewClient.HasErrors)
                        return;

                    if (NeedNewCompany)
                    {
                        NewCompany.Validate();
                        if (NewCompany.HasErrors)
                            return;

                        var companyExistCheck = repository
                            .Companies
                            .FirstOrDefault(x => x.Name == NewCompany.Name);

                        if (companyExistCheck != null)
                        {
                            MessageBox.Show($"Клиент {NewClient.Name} ({NewCompany.Name}) уже существует!");
                            return;
                        }

                        NewClient.Company = NewCompany;
                        eventAggregator.GetEvent<CompanyAddedEvent>().Publish(NewCompany);
                    }

                    var clientExistCheck = repository
                        .Clients
                        .FirstOrDefault(x => x.Name == NewClient.Name && x.Company.Name == NewClient.Company.Name);

                    if (clientExistCheck != null)
                    {
                        MessageBox.Show($"Клиент {NewClient.Name} ({NewClient.Company}) уже существует!");
                        return;
                    }

                    NewOrder.Client = NewClient;
                    eventAggregator.GetEvent<ClientAddedEvent>().Publish(NewClient);
                }

                repository.CreateOrder(NewOrder);
                eventAggregator.GetEvent<OrderAddedEvent>().Publish(NewOrder);

                NewOrder = new Order { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
                NewClient = new Client { Company = Companies.FirstOrDefault() };
                NewCompany = new Company();

                regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
            }
        }

        private void Cancel()
        {
            NewOrder = new Order { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
            NewClient = new Client { Company = Companies.FirstOrDefault() };
            NewCompany = new Company();

            regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
        }

        #endregion
    }
}