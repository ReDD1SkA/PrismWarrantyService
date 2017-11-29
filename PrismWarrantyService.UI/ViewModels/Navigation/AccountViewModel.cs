using System.Linq;
using System.Threading;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using PrismWarrantyService.Domain.Entities;
using Prism.Events;
using PrismWarrantyService.UI.Events;

namespace PrismWarrantyService.UI.ViewModels.Navigation
{
    public class AccountViewModel : ViewModelBase
    {
        #region Fields

        private Employee currentEmployee;

        #endregion

        #region Constructors and finalizers

        public AccountViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            eventAggregator.GetEvent<AuthenticationEvent>().Subscribe(AuthenticationHandler);
            AuthenticationHandler();

            LogoutCommand = new DelegateCommand(Logout);
        }

        #endregion

        #region Properties

        public Employee CurrentEmployee
        {
            get => currentEmployee;
            set => SetProperty(ref currentEmployee, value);
        }

        #endregion

        #region Commands

        public DelegateCommand LogoutCommand { get; private set; }

        #endregion

        #region Methods

        private void Logout()
        {
            if (Thread.CurrentPrincipal is CustomPrincipal customPrincipal)
            {
                CurrentEmployee = null;
                regionManager.RequestNavigate("AppRegion", "AuthenticationLayoutView");
                customPrincipal.Identity = new AnonymousIdentity();
            }
        }

        private void AuthenticationHandler()
        {
            CurrentEmployee = repository.Employees
                .Where(x => x.Login == Thread.CurrentPrincipal.Identity.Name)
                .FirstOrDefault();
        }

        #endregion
    }
}
