using PrismWarrantyService.Domain.Concrete;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Order : ValidatableBindableBase
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

        public virtual Client Client
        {
            get => client;
            set => SetProperty(ref client, value);
        }

        public int? EmployeeID { get; set; }

        public virtual Employee Employee
        {
            get => employee;
            set => SetProperty(ref employee, value);
        }

        public int? OrderStateID { get; set; }

        public virtual OrderState OrderState
        {
            get => orderState;
            set => SetProperty(ref orderState, value);
        }

        public int? OrderTypeID { get; set; }

        public virtual OrderType OrderType
        {
            get => orderType;
            set => SetProperty(ref orderType, value);
        }

        #endregion

        #region Methods

        public Order Clone()
        {
            return new Order()
            {
                Summary = Summary,
                Description = Description,
                Accepted = Accepted,
                Finished = Finished,
                Client = Client,
                Employee = Employee,
                OrderState = OrderState,
                OrderType = OrderType
            };
        }

        public void GetInfoFrom(Order donator)
        {
            Summary = donator.Summary;
            Description = donator.Description;
            Accepted = donator.Accepted;
            Finished = donator.Finished;
            Client = donator.Client;
            Employee = donator.Employee;
            OrderState = donator.OrderState;
            OrderType = donator.OrderType;
        }

        #endregion
    }
}