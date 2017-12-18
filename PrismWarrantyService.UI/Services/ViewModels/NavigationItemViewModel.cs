using Prism.Mvvm;

namespace PrismWarrantyService.UI.Services.ViewModels
{
    public class NavigationItemViewModel : BindableBase
    {
        #region Fields

        private string name;
        private string masterView;
        private string detailsView;

        #endregion

        #region Properties

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string MasterView
        {
            get => masterView;
            set => SetProperty(ref masterView, value);
        }

        public string DetailsView
        {
            get => detailsView;
            set => SetProperty(ref detailsView, value);
        }

        #endregion
    }
}
