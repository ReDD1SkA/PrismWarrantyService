using Prism.Mvvm;

namespace PrismWarrantyService.Domain.Entities
{
    public class Client : BindableBase
    {
        #region Fields

        private string name;
        private string company;
        private string email;
        private string phoneNumber;

        #endregion

        #region Properties

        public int ClientID { get; set; }

        public string Name
        {
            get =>  name;
            set => SetProperty(ref name, value);
        }

        public string Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }

        public string Email
        {
            get => email; 
            set => SetProperty(ref email, value); 
        }

        public string PhoneNumber
        {
            get => phoneNumber; 
            set => SetProperty(ref phoneNumber, value);
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