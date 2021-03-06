﻿using System;
using System.Threading;
using System.Windows.Controls;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Authentication;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Authentication
{
    public class AuthenticationViewModel : ViewModelBase
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private SnackbarViewModel _snackbar;
        private string _employeeLogin;

        #endregion

        #region Constructors and finalizers

        public AuthenticationViewModel(IAuthenticationService authenticationService, IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            _authenticationService = authenticationService;

            Snackbar = new SnackbarViewModel();

            LoginCommand = new DelegateCommand<PasswordBox>(Login);
        }

        #endregion

        #region Properties

        public string EmployeeLogin
        {
            get => _employeeLogin;
            set => SetProperty(ref _employeeLogin, value);
        }

        public SnackbarViewModel Snackbar
        {
            get => _snackbar;
            set => SetProperty(ref _snackbar, value);
        }

        #endregion

        #region Commands

        public DelegateCommand<PasswordBox> LoginCommand { get; }

        #endregion

        #region Methods

        private async void Login(PasswordBox passwordBox)
        {
            try
            {
                Employee emp = await _authenticationService.AuthenticateEmployeeAsync(EmployeeLogin, passwordBox.Password);

                if (!(Thread.CurrentPrincipal is CustomPrincipal customPrincipal))
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                customPrincipal.Identity = new CustomIdentity(emp.Login, emp.Role);
                eventAggregator.GetEvent<AuthenticationEvent>().Publish();
                await Task.Delay(1000);

                LoginCommand.RaiseCanExecuteChanged();

                EmployeeLogin = string.Empty;
                passwordBox.Password = string.Empty;

                switch(customPrincipal.Identity.Role.Name)
                {
                    case "Администратор":
                        regionManager.RequestNavigate("AppRegion", "AdminLayoutView");
                        break;
                    case "Пользователь":
                        regionManager.RequestNavigate("AppRegion", "UserLayoutView");
                        break;
                }                
            }
            catch (UnauthorizedAccessException)
            {
                Snackbar.Show("Неправильный логин или пароль!");
            }
        }

        #endregion
    }
}