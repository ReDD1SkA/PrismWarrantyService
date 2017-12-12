using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Clients.Admin
{
    public class ClientDetailsViewModel : ViewModelBase
    {
        #region Fields

        // Order fields
        private Client _originalSelectedClient;
        private Client _selectedClient;

        #endregion

        #region Constructors and finalizers

        public ClientDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {         
            // Properties init
            SelectedClient = repository.Clients.FirstOrDefault();

            // Events init
            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Subscribe(ClientSelectionChangedEventHandler);

            // Commands init
            UpdateClientCommand = new DelegateCommand(UpdateClient);
            UndoClientCommand = new DelegateCommand(UndoClient);
        }

        #endregion

        #region Properties

        // Client properties
        public Client OriginalSelectedClient
        {
            get => _originalSelectedClient;
            set => SetProperty(ref _originalSelectedClient, value);
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        #endregion

        #region Commands

        public DelegateCommand UpdateClientCommand { get; }
        public DelegateCommand UndoClientCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private async void UpdateClient()
        {
            SelectedClient.Validate();
            if (SelectedClient.HasErrors)
                return;

            OriginalSelectedClient.GetInfoFrom(SelectedClient);

            await Task.Run(() => repository.UpdateClient(OriginalSelectedClient));
        }

        private void UndoClient()
        {
            SelectedClient = OriginalSelectedClient.Clone();
        }

        // Event handlers
        private void ClientSelectionChangedEventHandler(Client parameter)
        {
            OriginalSelectedClient = parameter;
            SelectedClient = parameter.Clone();
        }

        #endregion
    }
}
