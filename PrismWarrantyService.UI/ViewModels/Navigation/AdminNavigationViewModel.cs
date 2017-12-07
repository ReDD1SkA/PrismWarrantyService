using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using MaterialDesignThemes.Wpf;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Navigation
{
    public class AdminNavigationViewModel : ViewModelBase
    {
        #region Fields

        private NavigationItemViewModel selectedItem;

        #endregion

        #region Constructors and finalizers

        public AdminNavigationViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            NavigateCommand = new DelegateCommand<NavigationItemViewModel>(Navigate);

            NavigationItems = new ObservableCollection<NavigationItemViewModel>
            {
                new NavigationItemViewModel() { Name = "Заказы", MasterView = "OrdersView", DetailsView = "OrderDetailsView"},
                new NavigationItemViewModel() { Name = "Клиенты", MasterView = "ClientsView", DetailsView = "ClientDetailsView"},
                new NavigationItemViewModel() { Name = "Компании", MasterView = "CompaniesView", DetailsView = "CompanyDetailsView"}
            };

            SelectedItem = NavigationItems.FirstOrDefault();
        }

        #endregion

        #region Properties

        public NavigationItemViewModel SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ObservableCollection<NavigationItemViewModel> NavigationItems { get; private set; }

        #endregion

        #region Commands

        public DelegateCommand<NavigationItemViewModel> NavigateCommand { get; private set; }

        #endregion

        #region Methods

        private void Navigate(NavigationItemViewModel item)
        {
            if (item.MasterView != null)
                regionManager.RequestNavigate("Admin.MasterRegion", item.MasterView);

            if (item.DetailsView != null)
                regionManager.RequestNavigate("Admin.DetailsRegion", item.DetailsView);

            DrawerHost.CloseDrawerCommand.Execute(null, null);
        }

        #endregion
    }
}