using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Authentication
{
    public class AuthenticationViewModel : ViewModelBase
    {
        #region Fields

        private IAuthenticationService authenticationService;
        private SnackbarViewModel snackbar;
        private string employeeLogin;

        #endregion

        #region Constructors and finalizers

        public AuthenticationViewModel(IAuthenticationService authenticationService, IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            this.authenticationService = authenticationService;

            Snackbar = new SnackbarViewModel();

            LoginCommand = new DelegateCommand<object>(Login);
        }

        #endregion

        #region Properties
        public string EmployeeLogin
        {
            get => employeeLogin;
            set => SetProperty(ref employeeLogin, value);
        }

        public SnackbarViewModel Snackbar
        {
            get => snackbar;
            set => SetProperty(ref snackbar, value);
        }

        #endregion

        #region Commands

        public DelegateCommand<object> LoginCommand { get; private set; }

        #endregion

        #region Methods

        private async void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            try
            {
                Employee emp = null;
                await Task.Factory.StartNew(() =>
                {
                    Employee answer = authenticationService.AuthenticateEmployee(EmployeeLogin, clearTextPassword);
                    emp = answer;
                });

                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                customPrincipal.Identity = new CustomIdentity(emp.Login, emp.Role);
                eventAggregator.GetEvent<AuthenticationEvent>().Publish();
                await Task.Delay(1000);

                LoginCommand.RaiseCanExecuteChanged();

                EmployeeLogin = string.Empty;
                passwordBox.Password = string.Empty;

                regionManager.RequestNavigate("AppRegion", "WorkspaceLayoutView");
            }
            catch (UnauthorizedAccessException)
            {
                Snackbar.Show("Неправильный логин или пароль!");
            }
            catch (Exception ex)
            {
                Snackbar.Show(string.Format("ERROR: {0}", ex.Message));
            }
        }

        #endregion
    }
}