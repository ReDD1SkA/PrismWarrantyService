using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismWarrantyService.UI.ViewModels.Navigation
{
    public class NavigationViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager regionManager;

        #endregion

        #region Constructors and finalizers

        public NavigationViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        #endregion

        #region Commands

        public DelegateCommand<string> NavigateCommand { get; private set; }

        #endregion

        #region Methods

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                regionManager.RequestNavigate("MainRegion", navigatePath);
        }

        #endregion
    }
}