﻿using Prism.Regions;
using PrismWarrantyService.UI.Views.Layouts;
using System.Threading.Tasks;

namespace PrismWarrantyService.UI.ViewModels
{
    public class ShellViewModel
    {
        #region Fields

        private IRegionManager regionManager;

        #endregion

        #region Constructors and finalizers

        public ShellViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion("AppRegion", typeof(AuthenticationLayoutView));
        }

        #endregion
    }
}