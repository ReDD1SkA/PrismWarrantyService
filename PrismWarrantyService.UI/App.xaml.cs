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
            RunBootstrapper();
        }

        private void RunBootstrapper()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
