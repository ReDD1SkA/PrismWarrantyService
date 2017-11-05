using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class AddOrderViewModel : BindableBase
    {
        #region Fields
        private IRepository repository;
        private Order newOrder;
        private Client newClient;
        #endregion

        #region Constructors and finalizers
        public AddOrderViewModel(IRepository repo)
        {
            repository = repo;

            Clients = new ObservableCollection<Client>(repository.Clients);
            OrderTypes = new ObservableCollection<OrderType>(repository.OrderTypes);

            NewOrder = new Order()
            {
                OrderType = OrderTypes.FirstOrDefault(),
                Client = Clients.FirstOrDefault()
            };
        }
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<OrderType> OrderTypes { get; set; }

        public Order NewOrder
        {
            get { return newOrder; }
            set { SetProperty(ref newOrder, value);  }
        }

        public Client NewClient
        {
            get { return newClient; }
            set { SetProperty(ref newClient, value); }
        }
        #endregion

        #region Commands
        #endregion

        #region Methods
        #endregion
    }
}
