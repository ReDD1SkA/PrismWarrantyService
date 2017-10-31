using Prism.Mvvm;

namespace PrismWarrantyService.Domain.Entities
{
    public class Employee : BindableBase
    {
        #region Fields
        private string login;
        private string hashedPassword;
        private string name;
        private string surname;
        private string position;
        private Role role;
        private Department department;
        #endregion

        #region Properties
        public int EmployeeID { get; set; }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }

        public string HashedPassword
        {
            get { return hashedPassword; }
            set
            {
                hashedPassword = value;
                RaisePropertyChanged("HashedPassword");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                RaisePropertyChanged("Position");
            }
        }

        public int RoleID { get; set; }

        public Role Role
        {
            get { return role; }
            set
            {
                role = value;
                RaisePropertyChanged("Role");
            }
        }

        public int DepartmentID { get; set; }

        public Department Department
        {
            get { return department; }
            set
            {
                department = value;
                RaisePropertyChanged("Department");
            }
        }
        #endregion
    }
}
