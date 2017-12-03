using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Role : ValidatableBindableBase
    {
        #region Fields

        private string name;

        #endregion

        #region Properties

        public int RoleID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Name
        {
            get => name; 
            set => SetProperty(ref name, value);
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