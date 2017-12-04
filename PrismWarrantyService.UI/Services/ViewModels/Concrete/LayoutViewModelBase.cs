using Prism.Regions;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class LayoutBaseViewModel
    {
        #region Fields

        protected IRegionManager regionManager;

        #endregion

        #region Constructors and finalizers

        public LayoutBaseViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            RegisterRegions();
        }

        #endregion

        #region Methods

        protected virtual void RegisterRegions() { }

        #endregion
    }
}
