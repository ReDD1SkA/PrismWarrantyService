using Prism.Mvvm;
using System;

namespace PrismWarrantyService.Domain.Entities
{
    public class Order : BindableBase
    {
        #region Fields
        private string summary;
        private string description;
        private DateTime accepted;
        private DateTime? finished;
        private Client client;
        private OrderState orderState;
        private OrderType orderType;
        #endregion

        #region Properties
        public int OrderID { get; set; }

        public string Summary
        {
            get { return summary; }
            set { SetProperty(ref summary, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public DateTime Accepted
        {
            get { return accepted; }
            set { SetProperty(ref accepted, value); }
        }

        public DateTime? Finished
        {
            get { return finished; }
            set { SetProperty(ref finished, value); }
        }

        public int ClientID { get; set; }

        public Client Client
        {
            get { return client; }
            set { SetProperty(ref client, value); }
        }

        public int? OrderStateID { get; set; }

        public OrderState OrderState
        {
            get { return orderState; }
            set { SetProperty(ref orderState, value); }
        }

        public int? OrderTypeID { get; set; }

        public OrderType OrderType
        {
            get { return orderType; }
            set { SetProperty(ref orderType, value); }
        }
        #endregion
    }
}