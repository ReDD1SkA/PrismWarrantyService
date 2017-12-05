using Prism.Regions;
using PrismWarrantyService.UI.Views.Navigation;
using PrismWarrantyService.UI.Views.User.Orders;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class UserWorkspaceLayoutViewModel : LayoutBaseViewModel
    {
        #region Constructors and finalizers

        public UserWorkspaceLayoutViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        #endregion

        #region Methods

        protected override void RegisterRegions()
        {
            // "navigation" regions
            regionManager.RegisterViewWithRegion("UserLayoutNavigationRegion", typeof(UserNavigationView));

            // "master" regions
            regionManager.RegisterViewWithRegion("UserLayoutMasterRegion", typeof(UserOrdersView));

            // "details" regions
            regionManager.RegisterViewWithRegion("UserLayoutDetailsRegion", typeof(UserOrderDetailsView));
        }

        #endregion
    }
}
