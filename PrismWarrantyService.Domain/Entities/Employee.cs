using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Employee : ValidatableBindableBase
    {
        #region Fields

        private string login;
        private string hashedPassword;
        private string firstName;
        private string lastName;
        private string surname;
        private string position;
        private Role role;
        private Department department;

        #endregion

        #region Properties

        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(12, ErrorMessage = "Максимальная длина - 12 символов")]
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

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string Surname
        {
            get => surname;
            set => SetProperty(ref surname, value);
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public int RoleID { get; set; }

        public virtual Role Role
        {
            get => role;
            set => SetProperty(ref role, value);
        }

        public int DepartmentID { get; set; }

        public virtual Department Department
        {
            get => department;
            set => SetProperty(ref department, value);
        }

        #endregion
    }
}
