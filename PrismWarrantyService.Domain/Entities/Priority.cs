﻿using PrismWarrantyService.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace PrismWarrantyService.Domain.Entities
{
    public class Priority : ValidatableBindableBase
    {
        #region Fields

        private string _name;

        #endregion

        #region Properties

        public int PriorityID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Name
        {
            get { return _name; }
            set { ValidateProperty(value); SetProperty(ref _name, value); }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}