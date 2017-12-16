using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Employee : ValidatableBindableBase
    {
        #region Fields

        private string _login;
        private string _hashedPassword;
        private string _firstName;
        private string _lastName;
        private string _surname;
        private string _position;
        private Role _role;

        #endregion

        #region Properties

        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(12, ErrorMessage = "Максимальная длина - 12 символов")]
        [RegularExpression(@"^[a-z0-9_-]{4,12}$", ErrorMessage = "Неверный формат логина")]
        public string Login
        {
            get { return _login; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _login, value);
            }
        }

        public string HashedPassword
        {
            get { return _hashedPassword; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _hashedPassword, value);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _firstName, value);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _lastName, value);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string Surname
        {
            get { return _surname; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _surname, value);
            }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string Position
        {
            get { return _position; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _position, value);
            }
        }

        public int RoleID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Role Role
        {
            get { return _role; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _role, value);
            }
        }

        #endregion

        #region  Methods

        public Employee Clone()
        {
            return new Employee
            {
                EmployeeID = EmployeeID,
                FirstName = FirstName,
                LastName = LastName,
                Surname = Surname,
                Position = Position,
                Login = Login,
                HashedPassword = HashedPassword,
                RoleID = RoleID,
                Role = Role
            };
        }

        public void GetInfoFrom(Employee donator)
        {
            EmployeeID = donator.EmployeeID;
            FirstName = donator.FirstName;
            LastName = donator.LastName;
            Surname = donator.Surname;
            Position = donator.Position;
            Login = donator.Login;
            HashedPassword = donator.HashedPassword;
            RoleID = donator.RoleID;
            Role = donator.Role;
        }

        public override string ToString()
        {
            return $"{Surname} {FirstName} {LastName}";
        }

        #endregion
    }
}
