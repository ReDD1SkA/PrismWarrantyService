using PrismWarrantyService.Domain.Concrete;
using System;
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

        public int OrderID { get; set; }

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
        public DateTime Accepted
        {
            get { return _accepted; }
            set { ValidateProperty(value); SetProperty(ref _accepted, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime Deadline
        {
            get { return _deadline; }
            set { ValidateProperty(value); SetProperty(ref _deadline, value); }
        }

        public DateTime? Finished
        {
            get { return _finished; }
            set { ValidateProperty(value); SetProperty(ref _finished, value); }
        }

        public int ClientID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Client Client
        {
            get { return _client; }
            set { ValidateProperty(value); SetProperty(ref _client, value); }
        }

        public int? StateID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual State State
        {
            get { return _state; }
            set { ValidateProperty(value); SetProperty(ref _state, value); }
        }

        public int? PriorityID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Priority Priority
        {
            get { return _priority; }
            set { ValidateProperty(value); SetProperty(ref _priority, value); }
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
                Deadline = Deadline,
                Finished = Finished,
                Client = Client,
                State = State,
                Priority = Priority
            };
        }

        public void GetInfoFrom(Order donator)
        {
            Summary = donator.Summary;
            Description = donator.Description;
            Accepted = donator.Accepted;
            Deadline = donator.Deadline;
            Finished = donator.Finished;
            Client = donator.Client;
            State = donator.State;
            Priority = donator.Priority;
        }

        #endregion
    }
}