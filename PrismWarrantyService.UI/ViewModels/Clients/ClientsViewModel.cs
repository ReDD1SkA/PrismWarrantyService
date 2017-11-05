using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismWarrantyService.UI.ViewModels.Clients
{
    public class ClientsViewModel : BindableBase
    {
        #region Fields
        private IRepository repository;
        private Client selectedClient;
        #endregion

        #region Constructors and finalizers
        public ClientsViewModel(IRepository repo)
        {
            repository = repo;
            Clients = new ObservableCollection<Client>(repository.Clients);
            Orders = new ObservableCollection<Order>();
            SelectedClient = Clients.FirstOrDefault();
        }
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get; set; }

        public ObservableCollection<Order> Orders { get; set; }

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
        #endregion
    }
}
