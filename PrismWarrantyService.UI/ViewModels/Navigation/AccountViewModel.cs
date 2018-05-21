using System.Linq;
using System.Threading;
using Prism.Commands;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Authentication;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using Prism.Events;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Navigation
{
    public class AccountViewModel : ViewModelBase
    {
        #region Fields

        private Employee _currentEmployee;

        #endregion

        #region Constructors and finalizers

        public AccountViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            eventAggregator.GetEvent<AuthenticationEvent>().Subscribe(AuthenticationEventHandler);
            AuthenticationEventHandler();

            LogoutCommand = new DelegateCommand(Logout);
        }

        #endregion

        #region Properties

        public Employee CurrentEmployee
        {
            get => _currentEmployee;
            set => SetProperty(ref _currentEmployee, value);
        }

        #endregion

        #region Commands

        public DelegateCommand LogoutCommand { get; }

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

        private void AuthenticationEventHandler()
        {
            CurrentEmployee = repo.GetEmployeeByLogin(Thread.CurrentPrincipal.Identity.Name);
        }

        #endregion
    }
}
