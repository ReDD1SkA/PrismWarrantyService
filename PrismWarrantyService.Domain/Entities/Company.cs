using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Company : ValidatableBindableBase
    {
        #region Fields

        private string name;
        private string swift;
        private string address;
        private string email;
        private string phoneNumber;

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

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(11, ErrorMessage = "Максимальная длина - 11 символов")]
        [RegularExpression(@"^[A-Z]{6}[A-Z,0-9]{5}$", ErrorMessage = "Неверный формат SWIFT-кода")]
        public string Swift
        {
            get { return swift; }
            set { ValidateProperty(value); SetProperty(ref swift, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        [RegularExpression(@"^[A-Z]{6}[A-Z,0-9]{5}$", ErrorMessage = "Неверный формат SWIFT-кода")]
        public string Address
        {
            get { return address; }
            set { ValidateProperty(value); SetProperty(ref address, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Неверный формат E-mail")]
        [StringLength(30, ErrorMessage = "Максимальная длина - 30 символов")]
        public string Email
        {
            get { return email; }
            set { ValidateProperty(value); SetProperty(ref email, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(16, ErrorMessage = "Максимальная длина - 16 символов")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Формат номера: XXX-XX-XXX-XX-XX")]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { ValidateProperty(value); SetProperty(ref phoneNumber, value); }
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
