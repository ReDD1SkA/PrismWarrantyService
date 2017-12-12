using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Lists;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Companies.Admin
{
    public class CreateCompanyViewModel : ViewModelBase
    {
        #region Fields

        private Company _newCompany;

        #endregion

        #region Constructors and finalizers

        public CreateCompanyViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Properties init
            NewCompany = new Company();

            // Commands init
            SaveCommand = new DelegateCommand(SaveCompany);
            CancelCommand = new DelegateCommand(Cancel);
        }

        #endregion

        #region Properties

        // Entities properties
        public Company NewCompany
        {
            get => _newCompany;
            set => SetProperty(ref _newCompany, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private async void SaveCompany()
        {
            NewCompany.Validate();
            if (NewCompany.HasErrors)
                return;

            var companyExistCheck = repository
                .Companies
                .FirstOrDefault(x => x.Name == NewCompany.Name);

            if (companyExistCheck != null)
            {
                MessageBox.Show($"Компания ({NewCompany.Name}) уже существует!");
                return;
            }

            await Task.Run(() => repository.CreateCompany(NewCompany));
            eventAggregator.GetEvent<NeedRefreshListsEvent>().Publish();

            NewCompany = new Company();

            regionManager.RequestNavigate("Admin.DetailsRegion", "CompanyDetailsView");
        }

        private void Cancel()
        {
            NewCompany = new Company();

            regionManager.RequestNavigate("Admin.DetailsRegion", "CompanyDetailsView");
        }

        #endregion
    }
}
