using Prism.Mvvm;

namespace PrismWarrantyService.Domain.Entities
{
    public class Client : BindableBase
    {
        #region Fields
        private string department;
        private string company;
        private string email;
        private string phoneNumber;
        #endregion

        #region Properties
        public int ClientID { get; set; }

        public string Department
        {
            get { return department; }
            set { SetProperty(ref department, value);  }
        }

        public string Company
        {
            get { return company; }
            set { SetProperty(ref company, value); }
        }

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }
        #endregion
    }
}