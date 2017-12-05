using Prism.Regions;
using PrismWarrantyService.UI.Views.Admin.Orders;
using PrismWarrantyService.UI.Views.Admin.Orders.AddOrder;
using PrismWarrantyService.UI.Views.Navigation;

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
            regionManager.RegisterViewWithRegion("AdminLayoutNavigationRegion", typeof(AdminNavigationView));

            // "master" regions
            regionManager.RegisterViewWithRegion("AdminLayoutMasterRegion", typeof(OrdersView));

            // "details" regions
            regionManager.RegisterViewWithRegion("AdminLayoutDetailsRegion", typeof(OrderDetailsView));
            regionManager.RegisterViewWithRegion("SelectOrderClientRegion", typeof(AddOrderSelectClientView));
            regionManager.RegisterViewWithRegion("SelectClientCompanyRegion", typeof(AddOrderSelectCompanyView));
        }

        #endregion
    }
}
