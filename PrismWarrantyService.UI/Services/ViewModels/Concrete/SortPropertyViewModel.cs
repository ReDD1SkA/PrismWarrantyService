using Prism.Mvvm;

namespace PrismWarrantyService.UI.Services.ViewModels.Concrete
{
    public class SortPropertyViewModel : BindableBase
    {
        #region Fields

        private string name;
        private string property;

        #endregion

        #region Properties

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Property
        {
            get => property;
            set => SetProperty(ref property, value);
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
