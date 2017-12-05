using Prism.Regions;
using PrismWarrantyService.UI.Views.Admin.Orders;
using PrismWarrantyService.UI.Views.Admin.Orders.AddOrder;
using PrismWarrantyService.UI.Views.Navigation;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class AdminLayoutViewModel : LayoutBaseViewModel
    {
        #region Constructors and finalizers

        public AdminLayoutViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        #endregion

        #region Methods

        protected override void RegisterRegions()
        {
            // "navigation" regions
            regionManager.RegisterViewWithRegion("Admin.NavigationRegion", typeof(AdminNavigationView));

            // "master" regions
            regionManager.RegisterViewWithRegion("Admin.MasterRegion", typeof(OrdersView));

            // "details" regions
            regionManager.RegisterViewWithRegion("Admin.DetailsRegion", typeof(OrderDetailsView));
            regionManager.RegisterViewWithRegion("Admin.AddOrder.SelectOrderClientRegion", typeof(AddOrderSelectClientView));
            regionManager.RegisterViewWithRegion("Admin.AddOrder.SelectClientCompanyRegion", typeof(AddOrderSelectCompanyView));
        }

        #endregion
    }
}
