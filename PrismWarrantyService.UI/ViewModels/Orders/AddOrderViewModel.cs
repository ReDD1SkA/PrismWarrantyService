using Prism.Commands;
using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;
using Prism.Events;
using PrismWarrantyService.UI.Events;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class AddOrderViewModel : BindableBase
    {
        #region Fields

        private IRepository repository;
        private IEventAggregator eventAggregator;
        private Order newOrder;
        private Client newClient;
        private bool needNewClient;

        #endregion

        #region Constructors and finalizers

        public AddOrderViewModel(IRepository repository, IEventAggregator eventAggregator)
        {
            this.repository = repository;
            this.eventAggregator = eventAggregator;

            Clients = new ObservableCollection<Client>(repository.Clients);
            OrderTypes = new ObservableCollection<OrderType>(repository.OrderTypes);
            OrderStates = new ObservableCollection<OrderState>(repository.OrderStates);

            NewClient = new Client();
            NewOrder = new Order();

            AddOrderCommand = new DelegateCommand(AddOrder);
        }

        #endregion

        #region Properties

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<OrderType> OrderTypes { get; set; }
        public ObservableCollection<OrderState> OrderStates { get; set; }

        public bool NeedNewClient
        {
            get => needNewClient;
            set => SetProperty(ref needNewClient, value);
        }

        public Order NewOrder
        {
            get => newOrder;
            set => SetProperty(ref newOrder, value);
        }

        public Client NewClient
        {
            get => newClient;
            set => SetProperty(ref newClient, value);
        }

        #endregion

        #region Commands

        public DelegateCommand AddOrderCommand { get; private set; }

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
                eventAggregator.GetEvent<ClientAddedEvent>().Publish(NewClient);
            }

            repository.AddOrder(NewOrder);
            eventAggregator.GetEvent<OrderAddedEvent>().Publish(NewOrder);
        }

        #endregion
    }
}
