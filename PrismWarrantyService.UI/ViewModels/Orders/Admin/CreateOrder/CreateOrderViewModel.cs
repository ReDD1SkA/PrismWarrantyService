using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Orders.Admin.CreateOrder
{
    public class CreateOrderViewModel : ViewModelBase
    {
        #region Fields

        private Order _newOrder;
        private Client _newClient;

        #endregion

        #region Constructors and finalizers

        public CreateOrderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            // Lists init
            Clients = new ObservableCollection<Client>(repo.GetAllClientsAsync().Result);
            Priorities = new ObservableCollection<Priority>(repo.GetAllPrioritiesAsync().Result);
            States = new ObservableCollection<State>(repo.GetAllStatesAsync().Result);

            // Properties init
            NewOrder = new Order { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
            NewClient = new Client();

            // Commands init
            SaveCommand = new DelegateCommand(SaveOrder);
            CancelCommand = new DelegateCommand(Cancel);
            ToSelectOrderClientCommand = new DelegateCommand(ToSelectOrderClient);
            ToAddNewClientToOrderCommand = new DelegateCommand(ToAddNewClientToOrder);
        }

        #endregion

        #region Properties

        public ObservableCollection<Client> Clients { get; }
        public ObservableCollection<Priority> Priorities { get; }
        public ObservableCollection<State> States { get; }

        public bool NeedNewClient { get; private set; }

        public Order NewOrder
        {
            get => _newOrder;
            set => SetProperty(ref _newOrder, value);
        }

        public Client NewClient
        {
            get => _newClient;
            set => SetProperty(ref _newClient, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand ToSelectOrderClientCommand { get; }
        public DelegateCommand ToAddNewClientToOrderCommand { get; }

        #endregion

        #region Methods

        // Navigation methods
        private void ToSelectOrderClient()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectOrderClientRegion", "CreateOrderSelectClientView");
            NeedNewClient = false;
        }

        private void ToAddNewClientToOrder()
        {
            regionManager.RequestNavigate("Admin.CreateOrder.SelectOrderClientRegion", "CreateOrderNewClientView");
            NeedNewClient = true;
        }

        // CRUD methods
        private async void SaveOrder()
        {
            NewOrder.Validate();
            if (NewOrder.IsValid)
            {
                if (NeedNewClient)
                {
                    NewClient.Validate();
                    if (NewClient.HasErrors)
                        return;

                    if (await repo.ClientAlreadyExistAsync(NewClient.Title) != null)
                        return;

                    NewOrder.Client = NewClient;
                }

                // TODO: async repo method
                await Task.Run(() => repo.CreateOrder(NewOrder));

                NewOrder = new Order { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
                NewClient = new Client();

                regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
            }
        }

        private void Cancel()
        {
            NewOrder = new Order { Client = Clients.FirstOrDefault(), Deadline = DateTime.Now };
            NewClient = new Client();

            regionManager.RequestNavigate("Admin.DetailsRegion", "OrderDetailsView");
        }

        #endregion
    }
}