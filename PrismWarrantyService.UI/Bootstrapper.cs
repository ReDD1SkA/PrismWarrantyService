using System;
using System.Windows;
using Ninject;
using Prism.Ninject;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Concrete;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using PrismWarrantyService.UI.Views;
using PrismWarrantyService.UI.Views.Admin.Clients;
using PrismWarrantyService.UI.Views.Admin.Orders;
using PrismWarrantyService.UI.Views.Admin.Orders.AddOrder;
using PrismWarrantyService.UI.Views.Layouts;
using PrismWarrantyService.UI.Views.User.Orders;

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

            AddBindings();
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

        private void AddBindings()
        {
            Kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InSingletonScope();
            Kernel.Bind<IRepository>().To<EFRepository>().InSingletonScope();
        }

        private void RegisterTypesForNavigation()
        {
            // layouts
            Kernel.RegisterTypeForNavigation<AuthenticationLayoutView>();
            Kernel.RegisterTypeForNavigation<AdminLayoutView>();
            Kernel.RegisterTypeForNavigation<UserLayoutView>();

            // admin layout views
            Kernel.RegisterTypeForNavigation<OrdersView>();
            Kernel.RegisterTypeForNavigation<OrderDetailsView>();
            Kernel.RegisterTypeForNavigation<AddOrderView>();
            Kernel.RegisterTypeForNavigation<AddOrderSelectClientView>();
            Kernel.RegisterTypeForNavigation<AddOrderNewClientView>();
            Kernel.RegisterTypeForNavigation<AddOrderSelectCompanyView>();
            Kernel.RegisterTypeForNavigation<AddOrderNewCompanyView>();
            Kernel.RegisterTypeForNavigation<ClientsView>();

            // employee layout views
            Kernel.RegisterTypeForNavigation<UserOrdersView>();
            Kernel.RegisterTypeForNavigation<UserOrderDetailsView>();
        }

        #endregion
    }
}