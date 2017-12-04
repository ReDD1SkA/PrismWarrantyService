using Prism.Regions;
using PrismWarrantyService.UI.Views.Admin.Orders;
using PrismWarrantyService.UI.Views.Admin.Orders.AddOrder;
using PrismWarrantyService.UI.Views.Navigation;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class EmployeeWorkspaceLayoutViewModel : LayoutBaseViewModel
    {
        #region Constructors and finalizers

        public EmployeeWorkspaceLayoutViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        #endregion

        #region Methods

        protected override void RegisterRegions()
        {
            // "navigation" regions
            regionManager.RegisterViewWithRegion("NavigationRegion", typeof(EmployeeNavigationView));

            // "master" regions
            // regionManager.RegisterViewWithRegion("MasterRegion", typeof(OrdersView));

            // "details" regions
            // regionManager.RegisterViewWithRegion("DetailsRegion", typeof(OrderDetailsView));
            // regionManager.RegisterViewWithRegion("SelectOrderClientRegion", typeof(AddOrderSelectClientView));
            // regionManager.RegisterViewWithRegion("SelectClientCompanyRegion", typeof(AddOrderSelectCompanyView));
        }

        #endregion
    }
}
