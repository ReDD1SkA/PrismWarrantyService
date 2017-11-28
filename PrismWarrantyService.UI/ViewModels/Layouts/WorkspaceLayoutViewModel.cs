﻿using Prism.Regions;
using PrismWarrantyService.UI.Views.Navigation;
using PrismWarrantyService.UI.Views.Orders;

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
            regionManager.RegisterViewWithRegion("MasterRegion", typeof(OrdersView));
            regionManager.RegisterViewWithRegion("DetailsRegion", typeof(OrderDetailsView));
            regionManager.RegisterViewWithRegion("SelectOrderClientRegion", typeof(SelectOrderClientView));
            regionManager.RegisterViewWithRegion("AccountRegion", typeof(AccountView));
        }

        #endregion
    }
}