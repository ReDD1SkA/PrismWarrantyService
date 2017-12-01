using Prism.Mvvm;
using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Client : ValidatableBindableBase
    {
        #region Fields

        private string name;
        private string company;
        private string email;
        private string phoneNumber;

        #endregion

        #region Properties

        public int ClientID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string Name
        {
            get { return name; }
            set { ValidateProperty(value); SetProperty(ref name, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string Company
        {
            get { return company; }
            set { ValidateProperty(value); SetProperty(ref company, value); }
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
            return string.Format("{0} ({1})", Name, Company);
        }

        #endregion
    }
}