using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;

namespace PrismWarrantyService.UI.ViewModels.Admin.Clients
{
    public class ClientDetailsViewModel : ViewModelBase
    {
        #region Fields

        private Client originalSelectedClient;
        private Client selectedClient;

        #endregion

        #region Constructors and finalizers

        public ClientDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            EditClientCommand = new DelegateCommand(EditClient);

            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Subscribe(ClientSelectionChangedEventHandler);

            OriginalSelectedClient = repository.Clients
                .FirstOrDefault();
            SelectedClient = OriginalSelectedClient.Clone();
        }

        #endregion

        #region Properties

        public Client OriginalSelectedClient
        {
            get => originalSelectedClient;
            set => SetProperty(ref originalSelectedClient, value);
        }

        public Client SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        #endregion

        #region Commands

        public DelegateCommand EditClientCommand { get; private set; }

        #endregion

        #region Methods

        private async void EditClient()
        {
            SelectedClient.Validate();
            if (SelectedClient.HasErrors)
                return;

            OriginalSelectedClient.GetInfoFrom(SelectedClient);

            await Task.Run(() => repository.EditClient(OriginalSelectedClient));
        }

        private void ClientSelectionChangedEventHandler(Client parameter)
        {
            OriginalSelectedClient = parameter;
            SelectedClient = parameter.Clone();
        }

        #endregion
    }
}
