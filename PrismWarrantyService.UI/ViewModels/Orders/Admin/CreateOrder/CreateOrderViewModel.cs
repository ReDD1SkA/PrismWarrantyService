using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Lists;
using PrismWarrantyService.UI.Services.ViewModels;

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
            // Lists init
            Clients = new ObservableCollection<Client>(repository.Clients);
            Priorities = new ObservableCollection<Priority>(repository.Priorities);
            States = new ObservableCollection<State>(repository.States);
            Companies = new ObservableCollection<Company>(repository.Companies);

            // Properties init
            NewOrder = new Order { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
            NewClient = new Client { Company = Companies.FirstOrDefault() };
            NewCompany = new Company();

            // Commands init
            SaveCommand = new DelegateCommand(SaveOrder);
            CancelCommand = new DelegateCommand(Cancel);
            ToSelectOrderClientCommand = new DelegateCommand(ToSelectOrderClient);
            ToAddNewClientToOrderCommand = new DelegateCommand(ToAddNewClientToOrder);
            ToSelectClientCompanyCommand = new DelegateCommand(ToSelectClientCompany);
            ToAddNewCompanyToClientCommand = new DelegateCommand(ToAddNewCompanyToClient);

            // Events init
            eventAggregator.GetEvent<NeedRefreshListsEvent>().Subscribe(NeedRefreshListsEventHandler, ThreadOption.UIThread);
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
        public DelegateCommand ToSelectOrderClientCommand { get; }
        public DelegateCommand ToAddNewClientToOrderCommand { get; }
        public DelegateCommand ToSelectClientCompanyCommand { get; }
        public DelegateCommand ToAddNewCompanyToClientCommand { get; }

        #endregion

        #region Methods

        // Navigation methods
        private void ToSelectOrderClient()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectOrderClientRegion", "CreateOrderSelectClientView");
            NeedNewClient = false;
        }

        private void ToAddNewClientToOrder()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectOrderClientRegion", "CreateOrderNewClientView");
            NeedNewClient = true;
        }

        private void ToSelectClientCompany()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectClientCompanyRegion", "CreateOrderSelectCompanyView");
            NeedNewCompany = false;
        }

        private void ToAddNewCompanyToClient()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectClientCompanyRegion", "CreateOrderNewCompanyView");
            NeedNewCompany = true;
        }

        // CRUD methods
        private async void SaveOrder()
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
                            return;

                        NewClient.Company = NewCompany;
                    }

                    var clientExistCheck = repository
                        .Clients
                        .FirstOrDefault(x => x.Name == NewClient.Name && x.Company.Name == NewClient.Company.Name);

                    if (clientExistCheck != null)
                        return;

                    NewOrder.Client = NewClient;
                }

                await Task.Run(() => repository.CreateOrder(NewOrder));
                eventAggregator.GetEvent<NeedRefreshListsEvent>().Publish();

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

        // Event handlers
        private void NeedRefreshListsEventHandler()
        {
            Clients.Clear();
            Companies.Clear();

            Clients.AddRange(repository.Clients);
            Companies.AddRange(repository.Companies);
        }

        #endregion
    }
}