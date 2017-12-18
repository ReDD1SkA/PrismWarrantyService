using System;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Companies;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Companies.Admin
{
    public class CompanyDetailsViewModel : ViewModelBase
    {
        #region Fields

        // Order fields
        private Company _originOfSelectedCompany;
        private Company _selectedCompany;

        #endregion

        #region Constructors and finalizers

        public CompanyDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Properties init
            OriginOfSelectedCompany = repository.Companies.First();
            SelectedCompany = OriginOfSelectedCompany.Clone();

            // Commands init
            UpdateCompanyCommand = new DelegateCommand(UpdateCompany);
            UndoCompanyCommand = new DelegateCommand(UndoCompany);

            // Events init
            eventAggregator.GetEvent<CompanySelectionChangedEvent>().Subscribe(CompanySelectionChangedEventHandler, ThreadOption.UIThread);
        }

        #endregion

        #region Properties

        // Client properties
        public Company OriginOfSelectedCompany
        {
            get => _originOfSelectedCompany;
            set => SetProperty(ref _originOfSelectedCompany, value);
        }

        public Company SelectedCompany
        {
            get => _selectedCompany;
            set => SetProperty(ref _selectedCompany, value);
        }

        #endregion

        #region Commands

        public DelegateCommand UpdateCompanyCommand { get; }
        public DelegateCommand UndoCompanyCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private async void UpdateCompany()
        {
            SelectedCompany.Validate();
            if (SelectedCompany.HasErrors)
                return;

            OriginOfSelectedCompany.GetInfoFrom(SelectedCompany);

            await Task.Run(() => repository.UpdateCompany(OriginOfSelectedCompany));
        }

        private void UndoCompany()
        {
            SelectedCompany = OriginOfSelectedCompany.Clone();
        }

        // Event handlers
        private void CompanySelectionChangedEventHandler(Company parameter)
        {
            OriginOfSelectedCompany = parameter;
            SelectedCompany = OriginOfSelectedCompany?.Clone();
        }

        #endregion
    }
}
