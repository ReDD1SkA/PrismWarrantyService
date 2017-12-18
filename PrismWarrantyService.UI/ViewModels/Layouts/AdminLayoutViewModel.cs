using Prism.Regions;
using PrismWarrantyService.UI.Services.ViewModels;
using PrismWarrantyService.UI.Views.Navigation;
using PrismWarrantyService.UI.Views.Orders.Admin.CreateOrder;
using PrismWarrantyService.UI.Views.Orders.Admin.OrderDetails;
using OrderDetailsView = PrismWarrantyService.UI.Views.Orders.Admin.OrderDetails.OrderDetailsView;
using OrdersView = PrismWarrantyService.UI.Views.Orders.Admin.OrdersView;

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
            regionManager.RegisterViewWithRegion("Admin.CreateOrder.SelectOrderClientRegion", typeof(CreateOrderSelectClientView));
            regionManager.RegisterViewWithRegion("Admin.CreateOrder.SelectClientCompanyRegion", typeof(CreateOrderSelectCompanyView));
            regionManager.RegisterViewWithRegion("Admin.CreateClient.SelectClientCompanyRegion", typeof(CreateOrderSelectCompanyView));
            regionManager.RegisterViewWithRegion("Admin.OrderDetails.Employees", typeof(OrderEmployeesView));
        }

        #endregion
    }
}
