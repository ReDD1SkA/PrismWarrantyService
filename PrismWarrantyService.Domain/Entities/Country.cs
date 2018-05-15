using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Country : ValidatableBindableBase
    {
        #region Fields

        private string _name;

        #endregion

        #region Properties

        public int CountryID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов")]
        public string Name
        {
            get { return _name; }
            set { ValidateProperty(value); SetProperty(ref _name, value); }
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