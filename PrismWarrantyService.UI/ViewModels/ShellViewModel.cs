using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Views.Layouts;
using System.Linq;

namespace PrismWarrantyService.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        #region Constructors and finalizers

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            repository.Employees
                .FirstOrDefault(x => x.Login == "initialize");

            regionManager.RegisterViewWithRegion("AppRegion", typeof(AuthenticationLayoutView));
        }

        #endregion
    }
}