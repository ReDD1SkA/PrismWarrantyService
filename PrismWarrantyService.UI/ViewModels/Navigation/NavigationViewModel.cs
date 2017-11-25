using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismWarrantyService.UI.Services.Navigation.Concrete;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismWarrantyService.UI.ViewModels.Navigation
{
    public class NavigationViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager regionManager;
        private NavigationItem selectedItem;

        #endregion

        #region Constructors and finalizers

        public NavigationViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);

            NavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem() { Name = "Заказы", View = "OrdersView"},
                new NavigationItem() { Name = "Клиенты", View = "ClientsView"}
            };

            SelectedItem = NavigationItems.FirstOrDefault();
        }

        #endregion

        #region Properties

        public NavigationItem SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ObservableCollection<NavigationItem> NavigationItems { get; private set; }

        #endregion

        #region Commands

        public DelegateCommand<string> NavigateCommand { get; private set; }

        #endregion

        #region Methods

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                regionManager.RequestNavigate("MasterRegion", navigatePath);
        }

        #endregion
    }
}