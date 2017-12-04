using Prism.Regions;
using PrismWarrantyService.UI.Views.Navigation;
using PrismWarrantyService.UI.Views.Orders;
using PrismWarrantyService.UI.Views.Orders.AddOrder;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class AdminWorkspaceLayoutViewModel : LayoutBaseViewModel
    {
        #region Constructors and finalizers

        public AdminWorkspaceLayoutViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        #endregion

        #region Methods

        protected override void RegisterRegions()
        {
            // "navigation" regions
            regionManager.RegisterViewWithRegion("NavigationRegion", typeof(AdminNavigationView));
            regionManager.RegisterViewWithRegion("AccountRegion", typeof(AccountView));

            // "master" regions
            regionManager.RegisterViewWithRegion("MasterRegion", typeof(OrdersView));

            // "details" regions
            regionManager.RegisterViewWithRegion("DetailsRegion", typeof(OrderDetailsView));
            regionManager.RegisterViewWithRegion("SelectOrderClientRegion", typeof(AddOrderSelectClientView));
            regionManager.RegisterViewWithRegion("SelectClientCompanyRegion", typeof(AddOrderSelectCompanyView));
        }

        #endregion
    }
}
