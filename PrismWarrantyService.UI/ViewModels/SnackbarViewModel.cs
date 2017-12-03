using Prism.Mvvm;
using System.Threading.Tasks;

namespace PrismWarrantyService.UI.ViewModels
{
    public class SnackbarViewModel : BindableBase
    {
        #region Fields

        private string content;
        private bool isActive;

        #endregion

        #region Constructors and finalizers

        public SnackbarViewModel()
        {
            IsActive = false;
        }

        #endregion

        #region Properties

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }

        #endregion

        #region Methods

        public async void Show(string content, int activeTime = 2000)
        {
            Content = content;
            IsActive = true;
            await Task.Delay(activeTime);
            IsActive = false;
        }

        #endregion
    }
}
