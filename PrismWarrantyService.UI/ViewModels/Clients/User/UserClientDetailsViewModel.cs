using System;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Clients;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Clients.User
{
    public class UserClientDetailsViewModel : ViewModelBase
    {
        #region Fields

        // Order fields
        private Client _selectedClient;

        #endregion

        #region Constructors and finalizers

        public UserClientDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Events init
            eventAggregator.GetEvent<ClientSelectionChangedEvent>().Subscribe(ClientSelectionChangedEventHandler, ThreadOption.UIThread);
        }

        #endregion

        #region Properties

        // Client properties

        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        #endregion

        #region Methods

        // Event handlers
        private void ClientSelectionChangedEventHandler(Client parameter)
        {
            SelectedClient = parameter;
        }

        #endregion
    }
}
