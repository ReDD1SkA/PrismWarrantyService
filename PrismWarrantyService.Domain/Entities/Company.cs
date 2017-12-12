using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Company : ValidatableBindableBase
    {
        #region Fields

        private string _name;
        private string _swift;
        private string _address;
        private string _email;
        private string _phoneNumber;

        #endregion

        #region Properties

        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        public string Name
        {
            get { return _name; }
            set { ValidateProperty(value); SetProperty(ref _name, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(11, ErrorMessage = "Максимальная длина - 11 символов")]
        [RegularExpression(@"^[A-Z]{6}[A-Z,0-9]{5}$", ErrorMessage = "Неверный формат SWIFT-кода")]
        public string Swift
        {
            get { return _swift; }
            set { ValidateProperty(value); SetProperty(ref _swift, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        public string Address
        {
            get { return _address; }
            set { ValidateProperty(value); SetProperty(ref _address, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Неверный формат E-mail")]
        [StringLength(30, ErrorMessage = "Максимальная длина - 30 символов")]
        public string Email
        {
            get { return _email; }
            set { ValidateProperty(value); SetProperty(ref _email, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(16, ErrorMessage = "Максимальная длина - 16 символов")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Формат номера: XXX-XX-XXX-XX-XX")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { ValidateProperty(value); SetProperty(ref _phoneNumber, value); }
        }

        #endregion

        #region Methods

        public Company Clone()
        {
            return new Company()
            {
                Name = Name,
                Swift = Swift,
                Address = Address,
                Email = Email,
                PhoneNumber = PhoneNumber
            };
        }

        public void GetInfoFrom(Company donator)
        {
            Name = donator.Name;
            Swift = donator.Swift;
            Address = donator.Address;
            Email = donator.Email;
            PhoneNumber = donator.PhoneNumber;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
