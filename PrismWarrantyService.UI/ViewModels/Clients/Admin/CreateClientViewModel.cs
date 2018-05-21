using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Clients.Admin
{
    public class CreateClientViewModel : ViewModelBase
    {
        #region Fields

        private Client _newClient;

        #endregion

        #region Constructors and finalizers

        public CreateClientViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            // Properties init
            NewClient = new Client();

            // Commands init
            SaveCommand = new DelegateCommand(SaveClient);
            CancelCommand = new DelegateCommand(Cancel);
        }

        #endregion

        #region Properties

        public Client NewClient
        {
            get => _newClient;
            set => SetProperty(ref _newClient, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private async void SaveClient()
        {
            NewClient.Validate();
            if (NewClient.HasErrors)
                return;

            if (repo.ClientAlreadyExistAsync(NewClient.Title) != null)
                return;
            
            await repo.CreateClientAsync(NewClient);

            eventAggregator.GetEvent<ClientListChangedEvent>().Publish();
            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        private void Cancel()
        {
            NewClient = new Client();
            regionManager.RequestNavigate("Admin.DetailsRegion", "ClientDetailsView");
        }

        #endregion
    }
}
