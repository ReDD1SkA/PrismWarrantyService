using Prism.Mvvm;
using System;

namespace PrismWarrantyService.Domain.Entities
{
    public class Remark : BindableBase
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
            get { return order; }
            set
            {
                order = value;
                RaisePropertyChanged("Order");
            }
        }

        public int EmployeeID { get; set; }

        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                RaisePropertyChanged("DateTime");
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged("Text");
            }
        }
        #endregion
    }
}
