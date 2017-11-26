using System;
using System.Windows;
using Ninject;
using Prism.Ninject;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Concrete;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using PrismWarrantyService.UI.Views;
using PrismWarrantyService.UI.Views.Clients;
using PrismWarrantyService.UI.Views.Orders;
using PrismWarrantyService.UI.Views.Layouts;

namespace PrismWarrantyService.UI
{
    public class Bootstrapper : NinjectBootstrapper
    {
        #region Methods

        protected override DependencyObject CreateShell()
        {
            return Kernel.Get<ShellView>();
        }

        protected override void ConfigureKernel()
        {
            base.ConfigureKernel();

            Kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InSingletonScope();
            Kernel.Bind<IRepository>().To<EFRepository>().InSingletonScope();

            RegisterTypesForNavigation();
            
        }

        protected override void InitializeModules()
        {
            AppDomain.CurrentDomain.SetThreadPrincipal(new CustomPrincipal());
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        private void RegisterTypesForNavigation()
        {
            Kernel.RegisterTypeForNavigation<AuthenticationLayoutView>();
            Kernel.RegisterTypeForNavigation<WorkspaceLayoutView>();
            Kernel.RegisterTypeForNavigation<OrdersView>();
            Kernel.RegisterTypeForNavigation<OrderDetailsView>();
            Kernel.RegisterTypeForNavigation<AddOrderView>();
            Kernel.RegisterTypeForNavigation<ClientsView>();
        }

        #endregion
    }
}