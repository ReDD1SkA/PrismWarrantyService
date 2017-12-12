using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;

namespace PrismWarrantyService.UI.Services.ViewModels.Concrete
{
    public class ViewModelBase : BindableBase
    {
        #region Fields

        protected IRegionManager regionManager;
        protected IEventAggregator eventAggregator;
        protected IRepository repository;

        #endregion

        #region Constructors and finalizers

        public ViewModelBase(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.repository = repository;
        }

        #endregion
    }
}