using System;
using System.Collections.ObjectModel;
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
        private string _email;
        private string _phoneNumber;
        private Room _room;
        private Position _position;
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
            set { ValidateProperty(value); SetProperty(ref _login, value); }
        }

        public string HashedPassword
        {
            get { return _hashedPassword; }
            set { ValidateProperty(value); SetProperty(ref _hashedPassword, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string FirstName
        {
            get { return _firstName; }
            set { ValidateProperty(value); SetProperty(ref _firstName, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string LastName
        {
            get { return _lastName; }
            set { ValidateProperty(value); SetProperty(ref _lastName, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string Surname
        {
            get { return _surname; }
            set { ValidateProperty(value); SetProperty(ref _surname, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Неверный формат E-mail")]
        [StringLength(320, ErrorMessage = "Максимальная длина - 320 символов")]
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

        public int RoomID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Room Room
        {
            get { return _room; }
            set { ValidateProperty(value); SetProperty(ref _room, value); }
        }

        public int PositionID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Position Position
        {
            get { return _position; }
            set { ValidateProperty(value); SetProperty(ref _position, value); }
        }

        public int RoleID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Role Role
        {
            get { return _role; }
            set { ValidateProperty(value); SetProperty(ref _role, value); }
        }

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        #endregion

        #region  Methods

        public Employee Clone()
        {
            return new Employee
            {
                EmployeeID = EmployeeID,
                Login = Login,
                HashedPassword = HashedPassword,
                FirstName = FirstName,
                LastName = LastName,
                Surname = Surname,
                Email = Email,
                PhoneNumber = PhoneNumber,
                RoomID = RoomID,
                Room = Room,
                PositionID = PositionID,
                Position = Position,
                RoleID = RoleID,
                Role = Role
            };
        }

        public void GetInfoFrom(Employee donator)
        {
            EmployeeID = donator.EmployeeID;
            Login = donator.Login;
            HashedPassword = donator.HashedPassword;
            FirstName = donator.FirstName;
            LastName = donator.LastName;
            Surname = donator.Surname;
            Email = donator.Email;
            PhoneNumber = donator.PhoneNumber;
            RoomID = donator.RoomID;
            Room = donator.Room;
            PositionID = donator.PositionID;
            Position = donator.Position;
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
