using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;

namespace PrismWarrantyService.UI.Services.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        #region Fields

        protected IRegionManager regionManager;
        protected IEventAggregator eventAggregator;
        protected IRepository repo;

        #endregion

        #region Constructors and finalizers

        public ViewModelBase(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.repo = repo;
        }

        #endregion
    }
}