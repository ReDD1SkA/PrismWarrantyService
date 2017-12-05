using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Services.Navigation.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            NavigateCommand = new DelegateCommand<string>(Navigate);

            NavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem() { Name = "Мои заказы", View = "UserOrdersView"}
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
                regionManager.RequestNavigate("UserLayoutMasterRegion", navigatePath);
        }

        #endregion
    }
}
