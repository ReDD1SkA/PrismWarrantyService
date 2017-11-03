using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

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
            // эта штука не хочет работать
            //Orders = (ObservableCollection<Order>)repository
            //    .Orders
            //    .Where(x => x.Client == SelectedClient);

            Orders.Clear();
            Orders.Add(new Order() { Summary = "Этот список имитирует" });
            Orders.Add(new Order() { Summary = "список заказов клиента" });
            Orders.Add(new Order() { Summary = SelectedClient.Department + " " + SelectedClient.Company });
        }
        #endregion
    }
}
