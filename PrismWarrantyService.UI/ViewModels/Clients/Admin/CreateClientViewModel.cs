using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Events.Companies;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Clients.Admin
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
            ToSelectClientCompanyCommand = new DelegateCommand(SelectClientCompany);
            ToAddNewCompanyToClientCommand = new DelegateCommand(AddNewCompanyToClient);

            // Events init
            eventAggregator.GetEvent<CompanyListChangedEvent>().Subscribe(CompanyListChangedEventHandler, ThreadOption.UIThread);
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
        public DelegateCommand ToSelectClientCompanyCommand { get; }
        public DelegateCommand ToAddNewCompanyToClientCommand { get; }

        #endregion

        #region Methods

        // Navigation methods
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

        // CRUD methods
        private async void SaveClient()
        {
            NewClient.Validate();
            if (NewClient.HasErrors)
                return;

            if (NeedNewCompany)
            {
                NewCompany.Validate();
                if (NewCompany.HasErrors)
                    return;

                if (repository.CompanyAlreadyExistAsync(NewCompany.Name) != null)
                    return;

                NewClient.Company = NewCompany;
            }

            if (repository.ClientAlreadyExistAsync(NewClient.Title, NewCompany.Name) != null)
                return;
            
            await repository.CreateClientAsync(NewClient);

            eventAggregator.GetEvent<ClientListChangedEvent>().Publish();
            if (NeedNewCompany) eventAggregator.GetEvent<CompanyListChangedEvent>().Publish();

            NewClient = new Client { Company = Companies.FirstOrDefault() };
            NewCompany = new Company();

            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        private void Cancel()
        {
            NewClient = new Client { Company = Companies.FirstOrDefault() };
            NewCompany = new Company();

            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        // Event handlers
        private void CompanyListChangedEventHandler()
        {
            Companies.Clear();
            Companies.AddRange(repository.Companies);
        }

        #endregion
    }
}
