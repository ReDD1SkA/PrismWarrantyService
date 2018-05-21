using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Employees;
using PrismWarrantyService.UI.Services.ViewModels;

namespace PrismWarrantyService.UI.ViewModels.Employees.Admin
{
    public class EmployeesViewModel : ViewModelBase
    {
        #region Fields

        // Orders fields
        private Employee _selectedEmployee;

        // Sort-filter fields
        private string _filterText;
        private SortPropertyViewModel _sortProperty = new SortPropertyViewModel { Name = "ID", Property = "EmployeeID" };
        private SortDirectionViewModel _sortDirection = new SortDirectionViewModel { Name = "По возрастанию", Direction = ListSortDirection.Ascending };

        #endregion

        #region Constructors and finalizers

        public EmployeesViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repo)
            : base(regionManager, eventAggregator, repo)
        {
            // Orders properties init

            EmployeesSource = new ObservableCollection<Employee>(repo.GetAllEmployees());
            Employees = new ListCollectionView(EmployeesSource);

            SelectedEmployee = Employees.CurrentItem as Employee;
            CheckedEmployees = new List<Employee>();

            // Sort-filters properties init
            SortProperties = new[]
            {
                SortProperty,
                new SortPropertyViewModel { Name = "Фамилия", Property = "Surname" },
                new SortPropertyViewModel { Name = "Имя", Property = "Name"},
                new SortPropertyViewModel { Name = "Логин", Property = "Login"},
                new SortPropertyViewModel { Name = "Должность", Property = "Position"},
                new SortPropertyViewModel { Name = "Статус", Property = "Role"}
            };

            SortDirections = new[]
            {
                SortDirection,
                new SortDirectionViewModel { Name = "По убыванию", Direction = ListSortDirection.Descending }
            };

            Employees.Filter = FilterBySummary;
            RefreshSort();

            // Commands init
            CreateEmployeeCommand = new DelegateCommand(CreateEmployee);
            DeleteEmployeeCommand = new DelegateCommand(DeleteEmployee);
            EmployeeSelectionChangedCommand = new DelegateCommand(EmployeeSelectionChanged);
            EmployeeCheckedCommand = new DelegateCommand<Employee>(EmployeeChecked);
            EmployeeUncheckedCommand = new DelegateCommand<Employee>(EmployeeUnchecked);

            // Events init
            eventAggregator.GetEvent<EmployeeSelectionChangedEvent>().Publish(SelectedEmployee);
        }

        #endregion

        #region Properties

        // Orders properties
        public ListCollectionView Employees { get; set; }

        public ObservableCollection<Employee> EmployeesSource { get; }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }

        public List<Employee> CheckedEmployees { get; set; }

        // Sort-filter properties
        public IEnumerable<SortPropertyViewModel> SortProperties { get; }

        public IEnumerable<SortDirectionViewModel> SortDirections { get; }

        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); Employees.Refresh(); }
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

        public DelegateCommand CreateEmployeeCommand { get; }
        public DelegateCommand DeleteEmployeeCommand { get; }
        public DelegateCommand EmployeeSelectionChangedCommand { get; }
        public DelegateCommand<Employee> EmployeeCheckedCommand { get; }
        public DelegateCommand<Employee> EmployeeUncheckedCommand { get; }

        #endregion

        #region Methods

        // CRUD methods
        private void CreateEmployee()
        {
            regionManager.RequestNavigate("Admin.DetailsRegion", "CreateEmployeeView");
        }

        private async void DeleteEmployee()
        {
            if (CheckedEmployees.Count == 0)
                return;

            foreach (var employee in CheckedEmployees)
            {
                await repo.DeleteEmployeeAsync(employee);
            }
            CheckedEmployees.Clear();
        }

        private void EmployeeChecked(Employee parameter)
        {
            CheckedEmployees.Add(parameter);
        }

        private void EmployeeUnchecked(Employee parameter)
        {
            CheckedEmployees.Remove(parameter);
        }

        // Event handlers
        private void EmployeeSelectionChanged()
        {
            eventAggregator.GetEvent<EmployeeSelectionChangedEvent>().Publish(SelectedEmployee);
            regionManager.RequestNavigate("Admin.DetailsRegion", "EmployeeDetailsView");
        }

        private void NeedRefreshListsEventHandler()
        {
            EmployeesSource.Clear();
            CheckedEmployees.Clear();
            EmployeesSource.AddRange(repo.GetAllEmployees());

            try
            {
                SelectedEmployee = Employees.GetItemAt(0) as Employee;
            }
            catch (ArgumentOutOfRangeException) { }

            RefreshSort();
        }

        // Sort-filter methods
        private void RefreshSort()
        {
            using (Employees.DeferRefresh())
            {
                Employees.SortDescriptions.Clear();
                Employees.SortDescriptions.Add(new SortDescription(SortProperty.Property, SortDirection.Direction));
            }
        }

        private bool FilterBySummary(object obj)
        {
            if (!(obj is Employee employee)) return false;
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            return employee.FirstName.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) != -1
                   || employee.LastName.IndexOf(FilterText, StringComparison.CurrentCultureIgnoreCase) != -1
                   || employee.Surname.IndexOf(FilterText, StringComparison.CurrentCultureIgnoreCase) != -1
                   || employee.Login.IndexOf(FilterText, StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        #endregion
    }
}
