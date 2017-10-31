using PrismWarrantyService.UI.Services.Authentification.Concrete;
using System;
using System.Windows;

namespace PrismWarrantyService.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CreateCustomPrincipal();
            RunBootstrapper();
        }

        private void CreateCustomPrincipal()
        {
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
        }

        private void RunBootstrapper()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
