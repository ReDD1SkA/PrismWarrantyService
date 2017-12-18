using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Companies;
using PrismWarrantyService.UI.Events.Lists;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Companies.Admin
{
    public class CompaniesViewModel : ViewModelBase
    {
        #region Fields

        // Companies fields
        private Company _selectedCompany;

        // Sort-filter fields
        private string _filterText;
        private SortPropertyViewModel _sortProperty = new SortPropertyViewModel { Name = "ID", Property = "CompanyID" };
        private SortDirectionViewModel _sortDirection = new SortDirectionViewModel { Name = "По возрастанию", Direction = ListSortDirection.Ascending };

        #endregion

        #region Constructors and finalizers

        public CompaniesViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Company properties init
            CompaniesSource = new ObservableCollection<Company>(repository.Companies);
            Companies = new ListCollectionView(CompaniesSource);

            SelectedCompany = Companies.CurrentItem as Company;
            CheckedCompanies = new List<Company>();

            // Company clients init
            CompanyClients = new ObservableCollection<Client>(repository.Clients.Where(x => x.CompanyID == SelectedCompany.CompanyID));

            // Sort-filters properties init
            SortProperties = new[]
            {
                SortProperty
            };

            SortDirections = new[]
            {
                SortDirection,
                new SortDirectionViewModel { Name = "По убыванию", Direction = ListSortDirection.Descending }
            };

            Companies.Filter = FilterByName;
            RefreshSort();

            // Commands init
            CreateCompanyCommand = new DelegateCommand(CreateCompany);
            DeleteCompanyCommand = new DelegateCommand(DeleteCompany);
            CompanySelectionChangedCommand = new DelegateCommand(CompanySelectionChanged);
            CompanyCheckedCommand = new DelegateCommand<Company>(CompanyChecked);
            CompanyUncheckedCommand = new DelegateCommand<Company>(CompanyUnchecked);

            // Events init
            eventAggregator.GetEvent<NeedRefreshListsEvent>().Subscribe(NeedRefreshListsEventHandler, ThreadOption.UIThread);

            eventAggregator.GetEvent<CompanySelectionChangedEvent>().Publish(SelectedCompany);
        }

        #endregion

        #region Properties

        // Company properties
        public ListCollectionView Companies { get; set; }

        public ObservableCollection<Company> CompaniesSource { get; set; }

        public Company SelectedCompany
        {
            get => _selectedCompany;
            set => SetProperty(ref _selectedCompany, value);
        }

        public List<Company> CheckedCompanies { get; set; }

        // Client order properties
        public ObservableCollection<Client> CompanyClients { get; set; }

        // Sort-filters properties
        public IEnumerable<SortPropertyViewModel> SortProperties { get; }

        public IEnumerable<SortDirectionViewModel> SortDirections { get; }

        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); Companies.Refresh(); }
        }

        public SortPropertyViewModel SortProperty
        {
            get { return _sortProperty; }
            set { SetProperty(ref _sortProperty, value); RefreshSort(); }
        }

        public SortDirectionViewModel SortDirection
        {
            get { return _sortDirection; }
            set { SetProperty(ref _sortDirection, value); RefreshSort(); }
        }

        #endregion

        #region Commands

        public DelegateCommand CreateCompanyCommand { get; }
        public DelegateCommand DeleteCompanyCommand { get; }
        public DelegateCommand CompanySelectionChangedCommand { get; }
        public DelegateCommand<Company> CompanyCheckedCommand { get; }
        public DelegateCommand<Company> CompanyUncheckedCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private void CreateCompany()
        {
            regionManager.RequestNavigate("Admin.DetailsRegion", "CreateCompanyView");
        }

        private async void DeleteCompany()
        {
            if (CheckedCompanies.Count == 0)
                return;

            foreach (var company in CheckedCompanies)
            {
                CompanyClients.Clear();
                await Task.Run(() => repository.DeleteCompany(company));
            }
            CheckedCompanies.Clear();

            eventAggregator.GetEvent<NeedRefreshListsEvent>().Publish();
        }

        private void CompanyChecked(Company parameter)
        {
            CheckedCompanies.Add(parameter);
        }

        private void CompanyUnchecked(Company parameter)
        {
            CheckedCompanies.Remove(parameter);
        }

        // Event handlers
        private void CompanySelectionChanged()
        {
            CompanyClients.Clear();
            if (SelectedCompany != null)
                CompanyClients.AddRange(repository.Clients.Where(x => x.CompanyID == SelectedCompany.CompanyID));

            eventAggregator.GetEvent<CompanySelectionChangedEvent>().Publish(SelectedCompany);
            regionManager.RequestNavigate("Admin.DetailsRegion", "CompanyDetailsView");
        }

        private void NeedRefreshListsEventHandler()
        {
            CompaniesSource.Clear();
            CompanyClients.Clear();
            CheckedCompanies.Clear();

            CompaniesSource.AddRange(repository.Companies);

            try
            {
                SelectedCompany = Companies.GetItemAt(0) as Company;
                CompanyClients.AddRange(repository.Clients.Where(x => x.CompanyID == SelectedCompany.CompanyID));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                MessageBox.Show(exception.Message);
            }

            RefreshSort();
        }

        // Sort-filter methods
        private void RefreshSort()
        {
            using (Companies.DeferRefresh())
            {
                Companies.SortDescriptions.Clear();
                Companies.SortDescriptions.Add(new SortDescription(SortProperty.Property, SortDirection.Direction));
            }
        }

        private bool FilterByName(object obj)
        {
            if (!(obj is Company company)) return false;
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            return company.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        #endregion
    }
}
