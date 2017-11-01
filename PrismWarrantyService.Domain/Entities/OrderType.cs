using Prism.Mvvm;

namespace PrismWarrantyService.Domain.Entities
{
    public class OrderType : BindableBase
    {
        #region Fields
        private string name;
        #endregion

        #region Properties
        public int OrderTypeID { get; set; }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        #endregion
    }
}