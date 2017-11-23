using PrismWarrantyService.Domain.Concrete;

namespace PrismWarrantyService.Domain.Entities
{
    public class Department : ModelBase
    {
        #region Fields

        private string name;

        #endregion

        #region Properties

        public int DepartmentID { get; set; }

        public string Name
        {
            get => name; 
            set => SetProperty(ref name, value);
        }

        #endregion
    }
}