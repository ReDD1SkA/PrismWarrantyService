using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using MaterialDesignThemes.Wpf;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Services.Navigation.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Navigation
{
    public class UserNavigationViewModel : ViewModelBase
    {
        #region Fields

        private NavigationItem selectedItem;

        #endregion

        #region Constructors and finalizers

        public UserNavigationViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            NavigateCommand = new DelegateCommand<NavigationItem>(Navigate);

            NavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem() { Name = "Мои заказы", MasterView = "UserOrdersView", DetailsView = ""}
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

        public DelegateCommand<NavigationItem> NavigateCommand { get; private set; }

        #endregion

        #region Methods

        private void Navigate(NavigationItem item)
        {
            if (item.MasterView != null)
                regionManager.RequestNavigate("User.MasterRegion", item.MasterView);

            if (item.DetailsView != null)
                regionManager.RequestNavigate("User.DetailsRegion", item.DetailsView);

            DrawerHost.CloseDrawerCommand.Execute(null, null);
        }

        #endregion
    }
}
