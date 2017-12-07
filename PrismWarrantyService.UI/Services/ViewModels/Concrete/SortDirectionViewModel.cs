using System.ComponentModel;
using Prism.Mvvm;

namespace PrismWarrantyService.UI.Services.ViewModels.Concrete
{
    public class SortDirectionViewModel : BindableBase
    {
        #region Fields

        private string name;
        private ListSortDirection direction;

        #endregion

        #region Properties

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public ListSortDirection Direction
        {
            get => direction;
            set => SetProperty(ref direction, value);
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
