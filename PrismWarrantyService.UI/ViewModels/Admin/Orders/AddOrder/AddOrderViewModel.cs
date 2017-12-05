using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;
using System;

namespace PrismWarrantyService.UI.ViewModels.Admin.Orders.AddOrder
{
    public class AddOrderViewModel : ViewModelBase
    {
        #region Fields

        private Order newOrder;
        private Client newClient;
        private Company newCompany;

        #endregion

        #region Constructors and finalizers

        public AddOrderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            Clients = new ObservableCollection<Client>(repository.Clients);
            Priorities = new ObservableCollection<Priority>(repository.Priorities);
            States = new ObservableCollection<State>(repository.States);
            Companies = new ObservableCollection<Company>(repository.Companies);

            NewOrder = new Order() { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
            NewClient = new Client() { Company = Companies.FirstOrDefault() };
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

        public bool NeedNewClient { get; private set; } = false;
        public bool NeedNewCompany { get; private set; } = false;

        public Order NewOrder
        {
            get => newOrder;
            set => SetProperty(ref newOrder, value);
        }

        public Client NewClient
        {
            get => newClient;
            set => SetProperty(ref newClient, value);
        }

        public Company NewCompany
        {
            get => newCompany;
            set => SetProperty(ref newCompany, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SelectOrderClientCommand { get; private set; }
        public DelegateCommand AddNewClientToOrderCommand { get; private set; }
        public DelegateCommand SelectClientCompanyCommand { get; private set; }
        public DelegateCommand AddNewCompanyToClientCommand { get; private set; }

        #endregion

        #region Methods

        private void SelectOrderClient()
        {
            regionManager.RequestNavigate("Admin.AddOrder.SelectOrderClientRegion", "AddOrderSelectClientView");
            NeedNewClient = false;
        }

        private void AddNewClientToOrder()
        {
            regionManager.RequestNavigate("Admin.AddOrder.SelectOrderClientRegion", "AddOrderNewClientView");
            NeedNewClient = true;
        }

        private void SelectClientCompany()
        {
            regionManager.RequestNavigate("Admin.AddOrder.SelectClientCompanyRegion", "AddOrderSelectCompanyView");
            NeedNewCompany = false;
        }

        private void AddNewCompanyToClient()
        {
            regionManager.RequestNavigate("Admin.AddOrder.SelectClientCompanyRegion", "AddOrderNewCompanyView");
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
                            .Where(x => x.Name == NewCompany.Name)
                            .FirstOrDefault();

                        if (companyExistCheck != null)
                        {
                            MessageBox.Show(string.Format("Клиент {0} ({1}) уже существует!", NewClient.Name, NewCompany.Name));
                            return;
                        }

                        NewClient.Company = NewCompany;
                        //eventAggregator.GetEvent<CompanyAddedEvent>().Publish(NewCompany);
                    }

                    var clientExistCheck = repository
                        .Clients
                        .Where(x => x.Name == NewClient.Name && x.Company.Name == NewClient.Company.Name)
                        .FirstOrDefault();

                    if (clientExistCheck != null)
                    {
                        MessageBox.Show(string.Format("Клиент {0} ({1}) уже существует!", NewClient.Name, NewClient.Company));
                        return;
                    }

                    NewOrder.Client = NewClient;
                    eventAggregator.GetEvent<ClientAddedEvent>().Publish(NewClient);
                }

                repository.AddOrder(NewOrder);
                eventAggregator.GetEvent<OrderAddedEvent>().Publish(NewOrder);

                NewOrder = new Order() { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
                NewClient = new Client();
                NewCompany = new Company();

                regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
            }
        }

        private void Cancel()
        {
            regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
        }

        #endregion
    }
}