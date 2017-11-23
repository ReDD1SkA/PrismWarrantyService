using PrismWarrantyService.Domain.Concrete;
using System;

namespace PrismWarrantyService.Domain.Entities
{
    public class Remark : ModelBase
    {
        #region Fields

        private Order order;
        private Employee employee;
        private DateTime dateTime;
        private string text;

        #endregion

        #region Properties

        public int RemarkID { get; set; }

        public int OrderID { get; set; }

        public Order Order
        {
            get => order;
            set => SetProperty(ref order, value);
        }

        public int EmployeeID { get; set; }

        public Employee Employee
        {
            get => employee;
            set => SetProperty(ref employee, value);
        }

        public DateTime DateTime
        {
            get => dateTime;
            set => SetProperty(ref dateTime, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        #endregion
    }
}
