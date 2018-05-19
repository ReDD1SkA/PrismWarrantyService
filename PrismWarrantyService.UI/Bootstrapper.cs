using System;
using System.Windows;
using Ninject;
using Prism.Ninject;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Concrete;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;
using PrismWarrantyService.UI.Views;
using PrismWarrantyService.UI.Views.Clients.Admin;
using PrismWarrantyService.UI.Views.Clients.User;
using PrismWarrantyService.UI.Views.Employees.Admin;
using PrismWarrantyService.UI.Views.Layouts;
using PrismWarrantyService.UI.Views.Orders.Admin;
using PrismWarrantyService.UI.Views.Orders.Admin.CreateOrder;
using PrismWarrantyService.UI.Views.Orders.Admin.OrderDetails;
using PrismWarrantyService.UI.Views.Orders.User;
using PrismWarrantyService.UI.Views.Orders.User.UserOrderDetails;

namespace PrismWarrantyService.UI
{
    public class Bootstrapper : NinjectBootstrapper
    {
        #region Methods

        // Overrided methods
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
            Application.Current.MainWindow = Shell as Window;
            Application.Current.MainWindow.Show();
        }

        // Helpers
        private void AddBindings()
        {
            Kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InSingletonScope();
            Kernel.Bind<IRepository>().To<EFRepository>().InSingletonScope();
        }

        private void RegisterTypesForNavigation()
        {
            // Layouts
            Kernel.RegisterTypeForNavigation<AuthenticationLayoutView>();
            Kernel.RegisterTypeForNavigation<AdminLayoutView>();
            Kernel.RegisterTypeForNavigation<UserLayoutView>();

            // Admin layout views
            // Orders views
            Kernel.RegisterTypeForNavigation<OrdersView>();
            Kernel.RegisterTypeForNavigation<OrderDetailsView>();
            Kernel.RegisterTypeForNavigation<OrderEmployeesView>();
            Kernel.RegisterTypeForNavigation<OrderAddEmployeeView>();
            Kernel.RegisterTypeForNavigation<CreateOrderView>();
            Kernel.RegisterTypeForNavigation<CreateOrderSelectClientView>();
            Kernel.RegisterTypeForNavigation<CreateOrderNewClientView>();
            Kernel.RegisterTypeForNavigation<CreateOrderSelectCompanyView>();
            Kernel.RegisterTypeForNavigation<CreateOrderNewCompanyView>();

            // Client views
            Kernel.RegisterTypeForNavigation<ClientsView>();
            Kernel.RegisterTypeForNavigation<ClientDetailsView>();
            Kernel.RegisterTypeForNavigation<CreateClientView>();

            // Employees views
            Kernel.RegisterTypeForNavigation<EmployeesView>();
            Kernel.RegisterTypeForNavigation<EmployeeDetailsView>();
            Kernel.RegisterTypeForNavigation<CreateEmployeeView>();

            // User layout views
            // Orders views
            Kernel.RegisterTypeForNavigation<UserOrdersView>();
            Kernel.RegisterTypeForNavigation<UserOrderDetailsView>();

            // Clients view
            Kernel.RegisterTypeForNavigation<UserClientsView>();
            Kernel.RegisterTypeForNavigation<UserClientDetailsView>();
        }

        #endregion
    }
}