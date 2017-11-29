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
using Prism.Regions;

namespace PrismWarrantyService.UI.ViewModels.Orders
{
    public class AddOrderViewModel : ViewModelBase
    {
        #region Fields

        private Order newOrder;
        private Client newClient;

        #endregion

        #region Constructors and finalizers

        public AddOrderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            Clients = new ObservableCollection<Client>(repository.Clients);
            OrderTypes = new ObservableCollection<OrderType>(repository.OrderTypes);
            OrderStates = new ObservableCollection<OrderState>(repository.OrderStates);

            NewClient = new Client();
            NewOrder = new Order() { Client = Clients.FirstOrDefault() };

            SaveCommand = new DelegateCommand(SaveOrder);
            CancelCommand = new DelegateCommand(Cancel);
            SelectOrderClientCommand = new DelegateCommand(SelectOrderClient);
            AddNewClientToOrderCommand = new DelegateCommand(AddNewClientToOrder);
        }

        #endregion

        #region Properties

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<OrderType> OrderTypes { get; set; }
        public ObservableCollection<OrderState> OrderStates { get; set; }

        public bool NeedNewClient { get; private set; } = false;

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

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SelectOrderClientCommand { get; private set; }
        public DelegateCommand AddNewClientToOrderCommand { get; private set; }

        #endregion

        #region Methods

        private void AddNewClientToOrder()
        {
            regionManager.RequestNavigate("SelectOrderClientRegion", "AddOrderNewClientView");
            NeedNewClient = true;
        }

        private void SelectOrderClient()
        {
            regionManager.RequestNavigate("SelectOrderClientRegion", "AddOrderSelectClientView");
            NeedNewClient = false;
        }

        private void SaveOrder()
        {
            NewOrder.Validate();
            if (NewOrder.IsValid)
            {
                // если создается новый клиент
                if (NeedNewClient)
                {
                    // проверка на валидность
                    NewClient.Validate();
                    if (NewClient.HasErrors)
                        return;

                    // проверка на новизну
                    var existCheck = repository
                        .Clients
                        .Where(x => x.Name == NewClient.Name && x.Company == NewClient.Company)
                        .FirstOrDefault();

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

                NewClient = new Client();
                NewOrder = new Order() { Client = Clients.FirstOrDefault() };

                regionManager.RequestNavigate("DetailsRegion", "OrderDetailsView");
            }
        }

        private void Cancel()
        {
            regionManager.RequestNavigate("DetailsRegion", "OrderDetailsView");
        }

        #endregion
    }
}