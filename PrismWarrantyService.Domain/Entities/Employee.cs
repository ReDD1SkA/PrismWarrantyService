using PrismWarrantyService.Domain.Concrete;

namespace PrismWarrantyService.Domain.Entities
{
    public class Employee : ModelBase
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
            get => login;
            set => SetProperty(ref login, value);
        }

        public string HashedPassword
        {
            get => hashedPassword;
            set => SetProperty(ref hashedPassword, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Surname
        {
            get => surname;
            set => SetProperty(ref surname, value);
        }

        public string Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public int RoleID { get; set; }

        public Role Role
        {
            get => role;
            set => SetProperty(ref role, value);
        }

        public int DepartmentID { get; set; }

        public Department Department
        {
            get => department;
            set => SetProperty(ref department, value);
        }

        #endregion
    }
}
