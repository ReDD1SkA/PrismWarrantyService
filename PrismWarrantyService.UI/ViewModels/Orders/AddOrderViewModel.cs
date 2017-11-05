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
            
            NewOrder = new Order() { OrderType = OrderTypes.FirstOrDefault(), Client = Clients.FirstOrDefault()};
            NewClient = new Client();

            addOrderCommand = new DelegateCommand(AddOrder);
        }
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<OrderType> OrderTypes { get; set; }

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
        private async void AddOrder()
        {
            if (NeedNewClient)
            {
                var existCheck = repository
                    .Clients
                    .Where(x => x.Name == NewClient.Name && x.Company == NewClient.Company)
                    .FirstOrDefault();

                if (existCheck != null)
                {
                    MessageBox.Show(string.Format("Клиент {0} ({1}) уже существует!", NewClient.Name, NewClient.Company));
                    return;
                }

                NewOrder.Client = NewClient;
            }

            // этому стоит быть в методе репозитория?
            // вообще все проверки вынести в методы репозитория?
            // и пусть кидается исключениями, которые я буду обрабатывать в модели представления?
            // и почему БД не хочет ставить дефолтное значение, если получает пустую ссылку?
            NewOrder.Accepted = DateTime.Now;
            NewOrder.OrderStateID = 1;

            await Task.Factory.StartNew(() => repository.AddOrder(NewOrder));
        }
        #endregion
    }
}
