using System.Linq;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.UI.Services.ViewModels;
using PrismWarrantyService.UI.Views.Layouts;

namespace PrismWarrantyService.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        #region Constructors and finalizers

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            // To accelerate authentication
            repo.Employees.FirstOrDefault();

            regionManager.RegisterViewWithRegion("AppRegion", typeof(AuthenticationLayoutView));
        }

        #endregion
    }
}