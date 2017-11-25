using Prism.Regions;
using PrismWarrantyService.UI.Views.Orders;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class AuthenticationLayoutViewModel : LayoutBaseViewModel
    {
        #region Constructors and finalizers

        public AuthenticationLayoutViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        #endregion

        #region Methods

        protected override void RegisterRegions()
        {
            regionManager.RegisterViewWithRegion("MasterRegion", typeof(OrdersView));
            regionManager.RegisterViewWithRegion("DetailsRegion", typeof(OrderDetailsView));
        }

        #endregion
    }
}
