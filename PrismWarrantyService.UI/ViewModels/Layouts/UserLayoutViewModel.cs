using Prism.Regions;
using PrismWarrantyService.UI.Views.Navigation;
using UserOrderDetailsView = PrismWarrantyService.UI.Views.Orders.User.UserOrderDetailsView;
using UserOrdersView = PrismWarrantyService.UI.Views.Orders.User.UserOrdersView;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class UserLayoutViewModel : LayoutBaseViewModel
    {
        #region Constructors and finalizers

        public UserLayoutViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        #endregion

        #region Methods

        protected override void RegisterRegions()
        {
            // "navigation" regions
            regionManager.RegisterViewWithRegion("User.NavigationRegion", typeof(UserNavigationView));

            // "master" regions
            regionManager.RegisterViewWithRegion("User.MasterRegion", typeof(UserOrdersView));

            // "details" regions
            regionManager.RegisterViewWithRegion("User.DetailsRegion", typeof(UserOrderDetailsView));
        }

        #endregion
    }
}
