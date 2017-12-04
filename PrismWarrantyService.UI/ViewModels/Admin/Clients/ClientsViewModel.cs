using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;
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

            eventAggregator.GetEvent<ClientAddedEvent>().Subscribe(ClientAddedHandler);
        }

        #endregion

        #region Properties

        public ObservableCollection<Client> Clients { get; private set; }

        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                SetProperty(ref selectedClient, value);
                //RefreshClientOrders();
            }
        }

        #endregion

        #region Commands
        #endregion

        #region Methods

        //private void RefreshClientOrders()
        //{
        //    Orders.Clear();
        //    Orders.AddRange(repository
        //        .Orders
        //        .Where(x => x.ClientID == SelectedClient.ClientID)
        //        .ToList());
        //}

        private void ClientAddedHandler(Client newClient)
        {
            Clients.Add(newClient);
        }

        #endregion
    }
}
