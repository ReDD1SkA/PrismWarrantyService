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
        private DateTime deadline;
        private DateTime? finished;
        private Client client;
        private State state;
        private Priority priority;

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

        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime Accepted
        {
            get => accepted;
            set => SetProperty(ref accepted, value);
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime Deadline
        {
            get => deadline;
            set => SetProperty(ref deadline, value);
        }

        public DateTime? Finished
        {
            get => finished;
            set => SetProperty(ref finished, value);
        }

        public int ClientID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Client Client
        {
            get => client;
            set => SetProperty(ref client, value);
        }

        public int? StateID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual State State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        public int? PriorityID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Priority Priority
        {
            get => priority;
            set => SetProperty(ref priority, value);
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