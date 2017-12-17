using PrismWarrantyService.Domain.Concrete;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Order : ValidatableBindableBase
    {
        #region Fields

        private string _summary;
        private string _description;
        private DateTime _accepted;
        private DateTime _deadline;
        private DateTime? _finished;
        private Client _client;
        private State _state;
        private Priority _priority;

        #endregion

        #region Properties

        // Primary key
        public int OrderID { get; set; }

        // Simple properties
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов")]
        public string Summary
        {
            get { return _summary; }
            set { ValidateProperty(value); SetProperty(ref _summary, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(1000, ErrorMessage = "Максимальная длина - 1000 символов")]
        public string Description
        {
            get { return _description; }
            set { ValidateProperty(value); SetProperty(ref _description, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Accepted
        {
            get { return _accepted; }
            set { ValidateProperty(value); SetProperty(ref _accepted, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Deadline
        {
            get { return _deadline; }
            set { ValidateProperty(value); SetProperty(ref _deadline, value); }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Finished
        {
            get { return _finished; }
            set { ValidateProperty(value); SetProperty(ref _finished, value); }
        }

        // Navigation properties
        public int ClientID { get; set; }
        public int? StateID { get; set; }
        public int? PriorityID { get; set; }

        // Complex properties
        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Client Client
        {
            get { return _client; }
            set { ValidateProperty(value); SetProperty(ref _client, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual State State
        {
            get { return _state; }
            set { ValidateProperty(value); SetProperty(ref _state, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Priority Priority
        {
            get { return _priority; }
            set { ValidateProperty(value); SetProperty(ref _priority, value); }
        }

        public virtual ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        #endregion

        #region Methods

        public Order Clone()
        {
            return new Order()
            {
                OrderID = OrderID,
                Summary = Summary,
                Description = Description,
                Accepted = Accepted,
                Deadline = Deadline,
                Finished = Finished,
                ClientID = ClientID,
                Client = Client,
                StateID = StateID,
                State = State,
                PriorityID = PriorityID,
                Priority = Priority,
                Employees = Employees
            };
        }

        public void GetInfoFrom(Order donator)
        {
            OrderID = donator.OrderID;
            Summary = donator.Summary;
            Description = donator.Description;
            Accepted = donator.Accepted;
            Deadline = donator.Deadline;
            Finished = donator.Finished;
            ClientID = donator.ClientID;
            Client = donator.Client;
            StateID = donator.StateID;
            State = donator.State;
            PriorityID = donator.PriorityID;
            Priority = donator.Priority;
            Employees = donator.Employees;
        }

        #endregion
    }
}