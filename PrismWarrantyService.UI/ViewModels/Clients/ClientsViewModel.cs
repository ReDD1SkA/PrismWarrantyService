using Prism.Events;
using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismWarrantyService.UI.ViewModels.Clients
{
    public class ClientsViewModel : BindableBase
    {
        #region Fields

        private IRepository repository;
        private IEventAggregator eventAggregator;
        private Client selectedClient;

        #endregion

        #region Constructors and finalizers

        public ClientsViewModel(IRepository repository, IEventAggregator eventAggregator)
        {
            this.repository = repository;
            this.eventAggregator = eventAggregator;

            Clients = new ObservableCollection<Client>(repository.Clients);
            Orders = new ObservableCollection<Order>();
            SelectedClient = Clients.FirstOrDefault();

            eventAggregator.GetEvent<ClientAddedEvent>().Subscribe(ClientAddedHandler);
        }

        #endregion

        #region Properties

        public ObservableCollection<Client> Clients { get; private set; }

        public ObservableCollection<Order> Orders { get; private set; }

        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                SetProperty(ref selectedClient, value);
                RefreshClientOrders(); 
            }
        }

        #endregion

        #region Commands
        #endregion

        #region Methods

        private void RefreshClientOrders()
        {
            Orders.Clear();
            Orders.AddRange(repository
                .Orders
                .Where(x => x.ClientID == SelectedClient.ClientID)
                .ToList());
        }

        private void ClientAddedHandler(Client newClient)
        {
            Clients.Add(newClient);
        }

        #endregion
    }
}
