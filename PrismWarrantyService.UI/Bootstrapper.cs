using Ninject;
using Prism.Events;
using Prism.Ninject;
using System.Windows;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Concrete;
using PrismWarrantyService.UI.Views;
using PrismWarrantyService.UI.ViewModels;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.Authentification.Concrete;

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
            Kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            Kernel.Bind<IRepository>().To<EFRepository>().InSingletonScope();
            Kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            Kernel.Bind<ShellViewModel>().ToSelf();
            Kernel.Bind<OrderListViewModel>().ToSelf();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (ShellView)Shell;
            Application.Current.MainWindow.Show();
        }
        #endregion
    }
}
