using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Performer : ValidatableBindableBase
    {
        #region Fields

        private Order _order;
        private Employee _employee;

        #endregion

        #region Properties

        public int PerformerID { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Order Order
        {
            get { return _order; }
            set { ValidateProperty(value); SetProperty(ref _order, value); }
        }

        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Employee Employee
        {
            get { return _employee; }
            set { ValidateProperty(value); SetProperty(ref _employee, value); }
        }

        #endregion
    }
}
