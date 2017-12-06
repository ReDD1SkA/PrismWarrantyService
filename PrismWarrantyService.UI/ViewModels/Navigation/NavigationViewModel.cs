using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using MaterialDesignThemes.Wpf;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Services.Navigation.Concrete;
using System.Threading;
using PrismWarrantyService.UI.Services.Authentification.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Navigation
{
    public class NavigationViewModel : ViewModelBase
    {
        #region Fields

        private NavigationItem selectedItem;

        #endregion

        #region Constructors and finalizers

        public NavigationViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            NavigateCommand = new DelegateCommand<NavigationItem>(Navigate);

            AdminNavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem() { Name = "Заказы", MasterView = "OrdersView", DetailsView = "OrderDetailsView"},
                new NavigationItem() { Name = "Клиенты", MasterView = "ClientsView", DetailsView = "ClientDetailsView"},
                new NavigationItem() { Name = "Компании", MasterView = "CompaniesView", DetailsView = "CompanyDetailsView"}
            };

            UserNavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem() { Name = "Мои заказы", MasterView = "UserOrdersView", DetailsView = ""}
            };

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;


            switch (customPrincipal.Identity.Role.Name)
            {
                case "Администратор":
                    regionManager.RequestNavigate("MasterRegion", "OrdersView");
                    regionManager.RequestNavigate("DetailsRegion", "OrderDetailsView");
                    break;
                case "Пользователь":
                    regionManager.RequestNavigate("MasterRegion", "UserOrdersView");
                    regionManager.RequestNavigate("DetailsRegion", "UserOrderDetailsView");
                    break;
            }

            SelectedItem = NavigationItems.FirstOrDefault();
        }

        #endregion

        #region Properties

        public NavigationItem SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ObservableCollection<NavigationItem> AdminNavigationItems { get; private set; }

        public ObservableCollection<NavigationItem> UserNavigationItems { get; private set; }

        #endregion

        #region Commands

        public DelegateCommand<NavigationItem> NavigateCommand { get; private set; }

        #endregion

        #region Methods

        private void Navigate(NavigationItem item)
        {
            if (item.MasterView != null)
                regionManager.RequestNavigate("MasterRegion", item.MasterView);

            if (item.DetailsView != null)
                regionManager.RequestNavigate("DetailsRegion", item.DetailsView);

            DrawerHost.CloseDrawerCommand.Execute(null, null);
        }

        #endregion
    }
}