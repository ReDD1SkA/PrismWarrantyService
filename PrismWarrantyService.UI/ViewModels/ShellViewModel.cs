using Prism.Regions;
using PrismWarrantyService.UI.Views.Orders;

namespace PrismWarrantyService.UI.ViewModels
{
    public class ShellViewModel
    {
        #region Fields

        private IRegionManager regionManager;

        #endregion

        #region Constructors and finalizers

        public ShellViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            RegisterRegions();
        }

        #endregion

        #region Methods

        private void RegisterRegions()
        {
            regionManager.RegisterViewWithRegion("MainRegion", typeof(OrderListView));
            regionManager.RegisterViewWithRegion("DetailsRegion", typeof(OrderDetailsView));
        }

        #endregion
    }
}