using PrismWarrantyService.Domain.Concrete;

namespace PrismWarrantyService.Domain.Entities
{
    public class OrderState : ValidatableBindableBase
    {
        #region Fields

        private string name;

        #endregion

        #region Properties

        public int OrderStateID { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
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