using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Companies;

namespace PrismWarrantyService.UI.ViewModels.Admin.Companies
{
    public class CompaniesViewModel : ViewModelBase
    {
        #region Fields

        private Company selectedCompany;

        #endregion

        #region Constructors and finalizers

        public CompaniesViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            Companies = new ObservableCollection<Company>(repository.Companies);
            SelectedCompany = Companies.FirstOrDefault();

            eventAggregator.GetEvent<CompanyAddedEvent>().Subscribe(CompanyAddedEventHandler);
        }

        #endregion

        #region Properties

        public ObservableCollection<Company> Companies { get; private set; }

        public Company SelectedCompany
        {
            get { return selectedCompany; }
            set
            {
                SetProperty(ref selectedCompany, value);
            }
        }

        #endregion

        #region Commands



        #endregion

        #region Methods

        private void CompanyAddedEventHandler(Company newCompany)
        {
            Companies.Add(newCompany);
        }

        #endregion
    }
}
