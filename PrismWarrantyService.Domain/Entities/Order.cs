using PrismWarrantyService.Domain.Concrete;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Order : ModelBase
    {
        #region Fields

        private string summary;
        private string description;
        private DateTime accepted;
        private DateTime? finished;
        private Client client;
        private Employee employee;
        private OrderState orderState;
        private OrderType orderType;

        #endregion

        #region Properties

        public int OrderID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов")]
        public string Summary
        {
            get { return summary; }
            set { ValidateProperty(value); SetProperty(ref summary, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(1000, ErrorMessage = "Максимальная длина - 1000 символов")]
        public string Description
        {
            get { return description; }
            set { ValidateProperty(value); SetProperty(ref description, value); }
        }

        public DateTime Accepted
        {
            get => accepted;
            set => SetProperty(ref accepted, value);
        }

        public DateTime? Finished
        {
            get => finished;
            set => SetProperty(ref finished, value);
        }

        public int ClientID { get; set; }

        public Client Client
        {
            get => client;
            set => SetProperty(ref client, value);
        }

        public int? EmployeeID { get; set; }

        public Employee Employee
        {
            get => employee;
            set => SetProperty(ref employee, value);
        }

        public int? OrderStateID { get; set; }

        public OrderState OrderState
        {
            get => orderState;
            set => SetProperty(ref orderState, value);
        }

        public int? OrderTypeID { get; set; }

        public OrderType OrderType
        {
            get => orderType;
            set => SetProperty(ref orderType, value);
        }

        #endregion
    }
}