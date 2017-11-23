using PrismWarrantyService.Domain.Concrete;

namespace PrismWarrantyService.Domain.Entities
{
    public class Role : ModelBase
    {
        #region Fields

        private string name;

        #endregion

        #region Properties

        public int RoleID { get; set; }

        public string Name
        {
            get => name; 
            set => SetProperty(ref name, value);
        }

        #endregion
    }
}