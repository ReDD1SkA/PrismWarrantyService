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
            set
            {
                department = value;
                RaisePropertyChanged("Department");
            }
        }

        public string Company
        {
            get { return company; }
            set
            {
                company = value;
                RaisePropertyChanged("Company");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }


        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }
        #endregion
    }
}