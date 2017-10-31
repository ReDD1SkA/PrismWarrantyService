using Prism.Mvvm;
using System;

namespace PrismWarrantyService.Domain.Entities
{
    public class Order : BindableBase
    {
        #region Fields
        private string summary;
        private DateTime accepted;
        private DateTime finished;
        private Client client;
        private OrderState orderState;
        private OrderType orderType;
        #endregion

        #region Properties
        public int OrderID { get; set; }

        public string Summary
        {
            get { return summary; }
            set
            {
                summary = value;
                RaisePropertyChanged("Summary");
            }
        }

        public DateTime Accepted
        {
            get { return accepted; }
            set
            {
                accepted = value;
                RaisePropertyChanged("Accepted");
            }
        }

        public DateTime Finished
        {
            get { return finished; }
            set
            {
                finished = value;
                RaisePropertyChanged("Finished");
            }
        }

        public int ClientID { get; set; }

        public Client Client
        {
            get { return client; }
            set
            {
                client = value;
                RaisePropertyChanged("Client");
            }
        }

        public int OrderStateID { get; set; }

        public OrderState OrderState
        {
            get { return orderState; }
            set
            {
                orderState = value;
                RaisePropertyChanged("OrderState");
            }
        }

        public int OrderTypeID { get; set; }

        public OrderType OrderType
        {
            get { return orderType; }
            set
            {
                orderType = value;
                RaisePropertyChanged("OrderType");
            }
        }
        #endregion
    }
}