using Prism.Regions;
using PrismWarrantyService.UI.Views.Orders;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class WorkspaceLayoutViewModel
    {
        #region Fields

        private IRegionManager regionManager;

        #endregion

        #region Constructors and finalizers

        public WorkspaceLayoutViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            RegisterRegions();
        }

        #endregion

        #region Methods

        private void RegisterRegions()
        {
            regionManager.RegisterViewWithRegion("MasterRegion", typeof(OrdersView));
            regionManager.RegisterViewWithRegion("DetailsRegion", typeof(OrderDetailsView));
        }

        #endregion
    }
}
