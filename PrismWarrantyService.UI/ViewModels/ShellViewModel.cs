using Prism.Regions;
using PrismWarrantyService.UI.Views.Orders;

namespace PrismWarrantyService.UI.ViewModels
{
    public class ShellViewModel
    {
        private IRegionManager regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            RegisterRegions();
        }

        private void RegisterRegions()
        {
            regionManager.RegisterViewWithRegion("MainRegion", typeof(OrdersView));
        }
    }
}