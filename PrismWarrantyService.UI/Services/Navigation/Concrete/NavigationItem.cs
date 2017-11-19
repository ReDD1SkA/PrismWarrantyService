using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWarrantyService.UI.Services.Navigation.Concrete
{
    public class NavigationItem : BindableBase
    {
        #region Fields

        private string name;
        private string view;

        #endregion

        #region Properties

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string View
        {
            get => view;
            set => SetProperty(ref view, value);
        }

        #endregion
    }
}
