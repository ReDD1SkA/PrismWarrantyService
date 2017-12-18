using Prism.Regions;
using PrismWarrantyService.UI.Services.ViewModels;
using PrismWarrantyService.UI.Views.Authentication;

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
            // "authentication" regions
            regionManager.RegisterViewWithRegion("AuthenticationRegion", typeof(AuthenticationView));
        }

        #endregion
    }
}
