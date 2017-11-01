using Prism.Mvvm;

namespace PrismWarrantyService.Domain.Entities
{
    public class Role : BindableBase
    {
        #region Fields
        private string name;
        #endregion

        #region Properties
        public int RoleID { get; set; }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        #endregion
    }
}