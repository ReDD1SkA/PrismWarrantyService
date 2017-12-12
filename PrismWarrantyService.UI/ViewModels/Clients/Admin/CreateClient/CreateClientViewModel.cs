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
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Clients.Admin.CreateClient
{
    public class CreateClientViewModel : ViewModelBase
    {
        #region Fields

        private Client _newClient;
        private Company _newCompany;

        #endregion

        #region Constructors and finalizers

        public CreateClientViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Collections init
            Companies = new ObservableCollection<Company>(repository.Companies);

            // Properties init
            NewClient = new Client { Company = Companies.FirstOrDefault() };
            NewCompany = new Company();

            // Commands init
            SaveCommand = new DelegateCommand(SaveClient);
            CancelCommand = new DelegateCommand(Cancel);
            SelectClientCompanyCommand = new DelegateCommand(SelectClientCompany);
            AddNewCompanyToClientCommand = new DelegateCommand(AddNewCompanyToClient);
        }

        #endregion

        #region Properties

        // Collections properties
        public ObservableCollection<Company> Companies { get; set; }

        // Entities properties
        public bool NeedNewCompany { get; private set; }

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
        public DelegateCommand SelectClientCompanyCommand { get; }
        public DelegateCommand AddNewCompanyToClientCommand { get; }

        #endregion

        #region Methods

        private void SelectClientCompany()
        {
            regionManager.RequestNavigate("Admin.CreateClient.SelectClientCompanyRegion", "CreateOrderSelectCompanyView");
            NeedNewCompany = false;
        }

        private void AddNewCompanyToClient()
        {
            regionManager.RequestNavigate("Admin.CreateClient.SelectClientCompanyRegion", "CreateOrderNewCompanyView");
            NeedNewCompany = true;
        }

        private void SaveClient()
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
                MessageBox.Show($"Клиент {NewClient.Name} ({NewClient.Company.Name}) уже существует!");
                return;
            }

            repository.CreateClient(NewClient);
            eventAggregator.GetEvent<ClientCreatedEvent>().Publish(NewClient);

            NewClient = new Client {Company = Companies.FirstOrDefault()};
            NewCompany = new Company();

            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        private void Cancel()
        {
            NewClient = new Client { Company = Companies.FirstOrDefault() };
            NewCompany = new Company();

            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        #endregion
    }
}
