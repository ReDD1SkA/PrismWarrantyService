using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Company : ValidatableBindableBase
    {
        #region Fields

        private string name;

        #endregion

        #region Properties

        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        public string Name
        {
            get { return name; }
            set { ValidateProperty(value); SetProperty(ref name, value); }
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
