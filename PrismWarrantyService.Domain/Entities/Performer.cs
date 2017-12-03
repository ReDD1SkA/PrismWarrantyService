using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Performer : ValidatableBindableBase
    {
        #region Fields

        private Order order;
        private Employee employee;

        #endregion

        #region Properties

        public int PerformerID { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Order Order
        {
            get => order;
            set => SetProperty(ref order, value);
        }

        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Employee Employee
        {
            get => employee;
            set => SetProperty(ref employee, value);
        }

        #endregion
    }
}
