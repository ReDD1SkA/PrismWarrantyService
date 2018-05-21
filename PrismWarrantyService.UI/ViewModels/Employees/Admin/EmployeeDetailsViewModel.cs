using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Employees;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Employees.Admin
{
    public class EmployeeDetailsViewModel : ViewModelBase
    {
        #region Fields

        // Order fields
        private Employee _originOfSelectedEmployee;
        private Employee _selectedEmployee;

        #endregion

        #region Constructors and finalizers

        public EmployeeDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            OriginOfSelectedEmployee = repo.GetAllEmployees().First();
            SelectedEmployee = OriginOfSelectedEmployee.Clone();

            Positions = new ObservableCollection<Position>(repo.GetAllPositions().OrderBy(x => x.Name));
            Roles = new ObservableCollection<Role>(repo.GetAllRoles().OrderBy(x => x.Name));
            Rooms = new ObservableCollection<Room>(repo.GetAllRooms().OrderBy(x => x.Name));
            
            UpdateEmployeeCommand = new DelegateCommand(UpdateEmployee);
            UndoEmployeeCommand = new DelegateCommand(UndoEmployee);
            
            eventAggregator.GetEvent<EmployeeSelectionChangedEvent>().Subscribe(EmployeeSelectionChangedEventHandler, ThreadOption.UIThread);
        }

        #endregion

        #region Properties

        public ObservableCollection<Position> Positions { get; set; }

        public ObservableCollection<Role> Roles { get; set; }

        public ObservableCollection<Room> Rooms { get; set; }

        public Employee OriginOfSelectedEmployee
        {
            get => _originOfSelectedEmployee;
            set => SetProperty(ref _originOfSelectedEmployee, value);
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }

        #endregion

        #region Commands

        public DelegateCommand UpdateEmployeeCommand { get; }
        public DelegateCommand UndoEmployeeCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private async void UpdateEmployee()
        {
            SelectedEmployee.Validate();
            if (SelectedEmployee.HasErrors)
                return;

            OriginOfSelectedEmployee.GetInfoFrom(SelectedEmployee);            
            await repo.UpdateEmployeeAsync(OriginOfSelectedEmployee);
        }

        private void UndoEmployee()
        {
            SelectedEmployee = OriginOfSelectedEmployee.Clone();
        }
        
        private void EmployeeSelectionChangedEventHandler(Employee parameter)
        {
            OriginOfSelectedEmployee = parameter;
            SelectedEmployee = OriginOfSelectedEmployee.Clone();
        }

        #endregion
    }
}
