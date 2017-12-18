using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

        public EmployeeDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Properties init
            OriginOfSelectedEmployee = repository.Employees.First();
            SelectedEmployee = OriginOfSelectedEmployee.Clone();

            Roles = new ObservableCollection<Role>(repository.Roles);

            // Commands init
            UpdateEmployeeCommand = new DelegateCommand(UpdateEmployee);
            UndoEmployeeCommand = new DelegateCommand(UndoEmployee);

            // Events init
            eventAggregator.GetEvent<EmployeeSelectionChangedEvent>().Subscribe(EmployeeSelectionChangedEventHandler, ThreadOption.UIThread);
        }

        #endregion

        #region Properties

        // Order properties
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

        public ObservableCollection<Role> Roles { get; set; }

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

            await Task.Run(() => repository.UpdateEmployee(OriginOfSelectedEmployee));
        }

        private void UndoEmployee()
        {
            SelectedEmployee = OriginOfSelectedEmployee.Clone();
        }

        // Event handlers
        private void EmployeeSelectionChangedEventHandler(Employee parameter)
        {
            OriginOfSelectedEmployee = parameter;
            SelectedEmployee = OriginOfSelectedEmployee?.Clone();
        }

        #endregion
    }
}
