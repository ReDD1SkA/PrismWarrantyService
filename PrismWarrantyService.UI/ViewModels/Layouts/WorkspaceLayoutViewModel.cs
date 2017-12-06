using Prism.Regions;
using PrismWarrantyService.UI.Views.Admin.Orders.AddOrder;

namespace PrismWarrantyService.UI.ViewModels.Layouts
{
    public class WorkspaceLayoutViewModel : LayoutBaseViewModel
    {
        #region Constructors and finalizers

        public WorkspaceLayoutViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        #endregion

        #region Methods

        protected override void RegisterRegions()
        {
            regionManager.RegisterViewWithRegion("AddOrder.SelectOrderClientRegion", typeof(AddOrderSelectClientView));
            regionManager.RegisterViewWithRegion("AddOrder.SelectClientCompanyRegion", typeof(AddOrderSelectCompanyView));
        }

        #endregion
    }
}
