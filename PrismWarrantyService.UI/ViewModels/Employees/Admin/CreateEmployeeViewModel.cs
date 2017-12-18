using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Lists;
using PrismWarrantyService.UI.Services.Authentification.Abstract;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Employees.Admin
{
    public class CreateEmployeeViewModel : ViewModelBase
    {
        #region Fields

        private IAuthenticationService _authenticationService;
        private Employee _newEmployee;

        #endregion

        #region Constructors and finalizers

        public CreateEmployeeViewModel(IAuthenticationService authenticationService, IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Services init
            _authenticationService = authenticationService;

            // Lists init
            Roles = new ObservableCollection<Role>(repository.Roles);

            // Properties init
            NewEmployee = new Employee();

            // Commands init
            SaveCommand = new DelegateCommand<PasswordBox>(SaveEmployee);
            CancelCommand = new DelegateCommand(Cancel);
        }

        #endregion

        #region Properties

        public ObservableCollection<Role> Roles { get; set; }

        public Employee NewEmployee
        {
            get => _newEmployee;
            set => SetProperty(ref _newEmployee, value);
        }

        #endregion

        #region Commands

        public DelegateCommand<PasswordBox> SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private async void SaveEmployee(PasswordBox passwordBox)
        {
            NewEmployee.Validate();
            if (NewEmployee.HasErrors)
                return;

            if (passwordBox.Password == string.Empty)
                return;

            var employeeExistCheck = repository.Companies
                .FirstOrDefault(x => x.Name == NewEmployee.FirstName);

            if (employeeExistCheck != null)
                return;

            NewEmployee.HashedPassword = _authenticationService.CalculateHash(passwordBox.Password, NewEmployee.Login);

            await Task.Run(() => repository.CreateEmployee(NewEmployee));
            eventAggregator.GetEvent<NeedRefreshListsEvent>().Publish();

            NewEmployee = new Employee();

            regionManager.RequestNavigate("Admin.DetailsRegion", "EmployeeDetailsView");
        }

        private void Cancel()
        {
            NewEmployee = new Employee { Role = Roles.First(x => x.Name == "Пользователь") };
            regionManager.RequestNavigate("Admin.DetailsRegion", "EmployeeDetailsView");
        }

        #endregion
    }
}
