using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismWarrantyService.UI.ViewModels.Admin.Clients
{
    public class ClientsViewModel : ViewModelBase
    {
        #region Fields

        private Client selectedClient;

        #endregion

        #region Constructors and finalizers

        public ClientsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            Clients = new ObservableCollection<Client>(repository.Clients);
            SelectedClient = Clients.FirstOrDefault();

            ClientSelectionChangedCommand = new DelegateCommand(ClientSelectionChanged);

            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
            eventAggregator.GetEvent<ClientAddedEvent>().Subscribe(ClientAddedEventHandler);
        }

        #endregion

        #region Properties

        public ObservableCollection<Client> Clients { get; private set; }

        public Client SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        #endregion

        #region Commands

        public DelegateCommand ClientSelectionChangedCommand { get; private set; }

        #endregion

        #region Methods

        private void ClientSelectionChanged()
        {
            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Publish(SelectedClient);
        }

        private void ClientAddedEventHandler(Client newClient)
        {
            Clients.Add(newClient);
            SelectedClient = Clients.LastOrDefault();
        }

        #endregion
    }
}
