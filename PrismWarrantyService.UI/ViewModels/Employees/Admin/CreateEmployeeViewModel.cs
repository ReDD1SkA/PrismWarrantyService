using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
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

        public CreateEmployeeViewModel(IAuthenticationService authenticationService, IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            // Services init
            _authenticationService = authenticationService;

            // Lists init
            Positions = new ObservableCollection<Position>(repo.GetAllPositions().OrderBy(x => x.Name));
            Roles = new ObservableCollection<Role>(repo.GetAllRoles().OrderBy(x => x.Name));
            Rooms = new ObservableCollection<Room>(repo.GetAllRooms().OrderBy(x => x.Name));

            // Properties init
            NewEmployee = new Employee();

            // Commands init
            SaveCommand = new DelegateCommand<PasswordBox>(SaveEmployee);
            CancelCommand = new DelegateCommand(Cancel);
        }

        #endregion

        #region Properties

        public ObservableCollection<Position> Positions { get; set; }

        public ObservableCollection<Role> Roles { get; set; }

        public ObservableCollection<Room> Rooms { get; set; }

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
            if (NewEmployee.HasErrors || passwordBox.Password == string.Empty)
                return;
            
            NewEmployee.HashedPassword = _authenticationService.CalculateHash(passwordBox.Password, NewEmployee.Login);
            await repo.CreateEmployeeAsync(NewEmployee);

            NewEmployee = new Employee();
            regionManager.RequestNavigate("Admin.DetailsRegion", "EmployeeDetailsView");
        }

        private void Cancel()
        {
            NewEmployee = new Employee ();
            regionManager.RequestNavigate("Admin.DetailsRegion", "EmployeeDetailsView");
        }

        #endregion
    }
}
