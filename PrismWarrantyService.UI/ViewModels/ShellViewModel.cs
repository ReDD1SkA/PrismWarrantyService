using Prism.Regions;
using PrismWarrantyService.UI.Views.Layouts;
using System.Threading.Tasks;

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
            regionManager.RegisterViewWithRegion("AppRegion", typeof(AuthenticationLayoutView));
            //regionManager.RegisterViewWithRegion("AppRegion", typeof(WorkspaceLayoutView));
        }

        #endregion
    }
}