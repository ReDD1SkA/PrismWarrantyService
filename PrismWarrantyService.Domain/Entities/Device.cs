using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Device : ValidatableBindableBase
    {
        #region Fields

        private string _title;
        private string _legalAddress;
        private string _mailingAddress;
        private string _swift;
        private string _checkingAccount;
        private string _taxPayerNumber;
        private string _phoneNumber;
        private string _email;

        #endregion

        #region Properties

        public int ClientID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(500, ErrorMessage = "Максимальная длина - 500 символов")]
        public string Title
        {
            get { return _title; }
            set { ValidateProperty(value); SetProperty(ref _title, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(250, ErrorMessage = "Максимальная длина - 250 символов")]
        public string LegalAddress
        {
            get { return _legalAddress; }
            set { ValidateProperty(value); SetProperty(ref _legalAddress, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(250, ErrorMessage = "Максимальная длина - 250 символов")]
        public string MailingAddress
        {
            get { return _mailingAddress; }
            set { ValidateProperty(value); SetProperty(ref _mailingAddress, value); }
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
        [StringLength(28, ErrorMessage = "Максимальная длина - 28 символов")]
        [RegularExpression(@"^[A-Z,0-9]{28}$", ErrorMessage = "Неверный формат расчетного счета")]
        public string CheckingAccount
        {
            get { return _checkingAccount; }
            set { ValidateProperty(value); SetProperty(ref _checkingAccount, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(9, ErrorMessage = "Максимальная длина - 9 символов")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Неверный формат УНП")]
        public string TaxPayerNumber
        {
            get { return _taxPayerNumber; }
            set { ValidateProperty(value); SetProperty(ref _taxPayerNumber, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(16, ErrorMessage = "Максимальная длина - 16 символов")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Формат номера: XXX-XX-XXX-XX-XX")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { ValidateProperty(value); SetProperty(ref _phoneNumber, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Неверный формат E-mail")]
        [StringLength(320, ErrorMessage = "Максимальная длина - 320 символов")]
        public string Email
        {
            get { return _email; }
            set { ValidateProperty(value); SetProperty(ref _email, value); }
        }

        #endregion

        #region Methods

        public Client Clone()
        {
            return new Client
            {
                ClientID = ClientID,
                Title = Title,
                LegalAddress = LegalAddress,
                MailingAddress = MailingAddress,
                Swift = Swift,
                CheckingAccount = CheckingAccount,
                TaxPayerNumber = TaxPayerNumber,
                PhoneNumber = PhoneNumber,
                Email = Email
            };
        }

        public void GetInfoFrom(Client donator)
        {
            ClientID = donator.ClientID;
            Title = donator.Title;
            LegalAddress = donator.LegalAddress;
            MailingAddress = donator.MailingAddress;
            Swift = donator.Swift;
            CheckingAccount = donator.CheckingAccount;
            TaxPayerNumber = donator.TaxPayerNumber;
            PhoneNumber = donator.PhoneNumber;
            Email = donator.Email;
        }

        public override string ToString()
        {
            return Title;
        }

        #endregion
    }
}