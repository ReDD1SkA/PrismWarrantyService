using System;
using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Device : ValidatableBindableBase
    {
        #region Fields

        private string _model;
        private string _inventoryNumber;
        private DateTime _releaseDate;
        private string _remark;
        private Producer _producer;

        #endregion

        #region Properties

        public int DeviceID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов")]
        public string Model
        {
            get { return _model; }
            set { ValidateProperty(value); SetProperty(ref _model, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина - 50 символов")]
        public string InventoryNumber
        {
            get { return _inventoryNumber; }
            set { ValidateProperty(value); SetProperty(ref _inventoryNumber, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set { ValidateProperty(value); SetProperty(ref _releaseDate, value); }
        }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(500, ErrorMessage = "Максимальная длина - 500 символов")]
        public string Remark
        {
            get { return _remark; }
            set { ValidateProperty(value); SetProperty(ref _remark, value); }
        }

        public int ProducerID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Producer Producer
        {
            get { return _producer; }
            set { ValidateProperty(value); SetProperty(ref _producer, value); }
        }

        #endregion

        #region Methods

        public Device Clone()
        {
            return new Device
            {
                DeviceID = DeviceID,
                Model = Model,
                InventoryNumber = InventoryNumber,
                ReleaseDate = ReleaseDate,
                Remark = Remark,
                ProducerID = ProducerID,
                Producer = Producer
            };
        }

        public void GetInfoFrom(Device donator)
        {
            DeviceID = donator.DeviceID;
            Model = donator.Model;
            InventoryNumber = donator.InventoryNumber;
            ReleaseDate = donator.ReleaseDate;
            Remark = donator.Remark;
            ProducerID = donator.ProducerID;
            Producer = donator.Producer;
        }

        public override string ToString()
        {
            return $"{Model} ({Producer})";
        }

        #endregion
    }
}