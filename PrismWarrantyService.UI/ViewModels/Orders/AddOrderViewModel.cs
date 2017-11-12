using Prism.Commands;
using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class AddOrderViewModel : BindableBase
    {
        #region Fields
        private IRepository repository;
        private Order newOrder;
        private Client newClient;
        private bool needNewClient;
        private DelegateCommand addOrderCommand;
        #endregion

        #region Constructors and finalizers
        public AddOrderViewModel(IRepository repo)
        {
            repository = repo;

            Clients = new ObservableCollection<Client>(repository.Clients);
            OrderTypes = new ObservableCollection<OrderType>(repository.OrderTypes);
            OrderStates = new ObservableCollection<OrderState>(repository.OrderStates);

            NewClient = new Client();
            NewOrder = new Order();     

            addOrderCommand = new DelegateCommand(AddOrder);
        }
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<OrderType> OrderTypes { get; set; }
        public ObservableCollection<OrderState> OrderStates { get; set; }

        public bool NeedNewClient
        {
            get { return needNewClient; }
            set { SetProperty(ref needNewClient, value); }
        }

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
        public DelegateCommand AddOrderCommand { get { return addOrderCommand; } }
        #endregion

        #region Methods
        private void AddOrder()
        {
            // если создается новый клиент
            if (NeedNewClient)
            {
                // проверяем, действительно ли новый
                var existCheck = repository
                    .Clients
                    .Where(x => x.Name == NewClient.Name && x.Company == NewClient.Company)
                    .FirstOrDefault();

                // нашелся такой клиент - сообщаем пользователю и выходим
                if (existCheck != null)
                {
                    MessageBox.Show(string.Format("Клиент {0} ({1}) уже существует!", NewClient.Name, NewClient.Company));
                    return;
                }

                // иначе связываем нового клиента с заказом
                NewOrder.Client = NewClient;
            }

            repository.AddOrder(NewOrder);
        }
        #endregion
    }
}
