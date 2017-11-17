﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using PrismWarrantyService.UI.Views;
using System.Threading.Tasks;

namespace PrismWarrantyService.UI.ViewModels.Authentication
{
    public class AuthenticationViewModel : BindableBase
    {
        #region Fields

        private IAuthenticationService authenticationService;
        private string employeeName;

        #endregion

        #region Constructors and finalizers

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
            LoginCommand = new DelegateCommand<object>(Login);
        }

        #endregion

        #region Properties

        public string EmployeeName
        {
            get { return employeeName; }
            set { SetProperty(ref employeeName, value); }
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
                await Task.Factory.StartNew(() => {
                    Employee answer  = authenticationService.AuthenticateEmployee(EmployeeName, clearTextPassword);
                    emp = answer;
                    });

                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                customPrincipal.Identity = new CustomIdentity(emp.Login, emp.Role);

                LoginCommand.RaiseCanExecuteChanged();

                EmployeeName = string.Empty;
                passwordBox.Password = string.Empty;

                Window shell = new ShellView();
                shell.Show();
                Application.Current.MainWindow.Close();
            }
            catch (SecurityException)
            {
                MessageBox.Show("Вы не авторизованы!");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Вход не выполнен! Предоставьте валидные данные!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ERROR: {0}", ex.Message));
            }
        }

        #endregion
    }
}