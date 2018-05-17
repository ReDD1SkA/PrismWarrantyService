using System;
using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Producer : ValidatableBindableBase
    {
        #region Fields

        private string _name;
        private Country _country;
        #endregion

        #region Properties

        public int ProducerID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов")]
        public string Name
        {
            get { return _name; }
            set { ValidateProperty(value); SetProperty(ref _name, value); }
        }

        public int CountryID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public virtual Country Country
        {
            get { return _country; }
            set { ValidateProperty(value); SetProperty(ref _country, value); }
        }

        #endregion

        #region Methods

        public Producer Clone()
        {
            return new Producer
            {
                ProducerID = ProducerID,
                Name = Name,
                CountryID = CountryID,
                Country = Country
            };
        }

        public void GetInfoFrom(Producer donator)
        {
            ProducerID = donator.ProducerID;
            Name = Name;
            CountryID = donator.CountryID;
            Country = donator.Country;
        }

        public override string ToString()
        {
            return $"{Name} ({Country.Name})";
        }

        #endregion
    }
}