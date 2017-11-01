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
            set { SetProperty(ref login, value); }
        }

        public string HashedPassword
        {
            get { return hashedPassword; }
            set { SetProperty(ref hashedPassword, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Surname
        {
            get { return surname; }
            set { SetProperty(ref surname, value); }
        }

        public string Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }

        public int RoleID { get; set; }

        public Role Role
        {
            get { return role; }
            set { SetProperty(ref role, value); }
        }

        public int DepartmentID { get; set; }

        public Department Department
        {
            get { return department; }
            set { SetProperty(ref department, value); }
        }
        #endregion
    }
}
