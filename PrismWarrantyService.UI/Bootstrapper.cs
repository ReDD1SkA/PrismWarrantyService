using System;
using Ninject;
using Prism.Ninject;
using System.Windows;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Concrete;
using PrismWarrantyService.UI.ViewModels.Authentication;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using PrismWarrantyService.UI.ViewModels.Orders;
using PrismWarrantyService.UI.Views;
using PrismWarrantyService.UI.Views.Authentication;

namespace PrismWarrantyService.UI
{
    public class Bootstrapper : NinjectBootstrapper
    {
        #region Methods
        protected override DependencyObject CreateShell()
        {
            //return Kernel.Get<AuthenticationView>();
            return Kernel.Get<ShellView>();
        }

        protected override void ConfigureKernel()
        {
            base.ConfigureKernel();

            Kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InSingletonScope();
            Kernel.Bind<IRepository>().To<EFRepository>().InSingletonScope();
            Kernel.Bind<AuthenticationViewModel>().ToSelf();
            Kernel.Bind<OrdersViewModel>().ToSelf();
        }

        protected override void InitializeModules()
        {
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
        #endregion
    }
}
