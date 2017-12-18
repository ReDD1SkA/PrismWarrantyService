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

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // To accelerate authentication
            repository.Employees.FirstOrDefault();

            regionManager.RegisterViewWithRegion("AppRegion", typeof(AuthenticationLayoutView));
        }

        #endregion
    }
}