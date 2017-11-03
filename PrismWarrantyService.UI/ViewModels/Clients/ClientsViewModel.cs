using Prism.Mvvm;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get; set; }

        public Client SelectedClient
        {
            get { return selectedClient; }
            set { SetProperty(ref selectedClient, value); }
        }
        #endregion

        #region Commands
        #endregion

        #region Methods
        #endregion
    }
}
