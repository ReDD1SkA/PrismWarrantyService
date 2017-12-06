using Prism.Mvvm;
using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Client : ValidatableBindableBase
    {
        #region Fields

        private string name;
        private string email;
        private string phoneNumber;
        private Company company;

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

        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Company Company
        {
            get { return company; }
            set { ValidateProperty(value); SetProperty(ref company, value); }
        }

        #endregion

        #region Methods

        public Client Clone()
        {
            return new Client()
            {
                Name = Name,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Company = Company
            };
        }

        public void GetInfoFrom(Client donator)
        {
            Name = donator.Name;
            Email = donator.Email;
            PhoneNumber = donator.PhoneNumber;
            Company = donator.Company;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Company.Name);
        }

        #endregion
    }
}