using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PrismWarrantyService.Domain.Abstract;
using PrismWarrantyService.Domain.Entities;
using PrismWarrantyService.UI.Events.Lists;
using PrismWarrantyService.UI.Events.Orders;
using PrismWarrantyService.UI.Services.ViewModels.Concrete;

namespace PrismWarrantyService.UI.ViewModels.Orders.Admin.OrderDetails
{
    public class OrderDetailsViewModel : ViewModelBase
    {
        #region Fields

        // Order fields
        private Order _originalSelectedOrder;
        private Order _selectedOrder;
        private Employee _selectedEmployee;

        #endregion

        #region Constructors and finalizers

        public OrderDetailsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IRepository repository)
            : base(regionManager, eventAggregator, repository)
        {
            // Properties init
            OriginOfSelectedOrder = repository.Orders.First();
            SelectedOrder = OriginOfSelectedOrder.Clone();

            var orderEmployees = repository.Performers
                .Where(x => x.OrderID == SelectedOrder.OrderID)
                .Select(x => x.Employee);

            Employees = new ObservableCollection<Employee>(orderEmployees);
            SelectedEmployee = Employees.FirstOrDefault();

            States = new ObservableCollection<State>(repository.States);
            Priorities = new ObservableCollection<Priority>(repository.Priorities);

            // Events init
            eventAggregator.GetEvent<OrderSelectionChangedEvent>().Subscribe(OrderSelectionChangedEventHandler);
            eventAggregator.GetEvent<NeedRefreshListsEvent>().Subscribe(NeedRefreshListsEventHandler);

            // Commands init
            UpdateOrderCommand = new DelegateCommand(UpdateOrder);
            UndoOrderCommand = new DelegateCommand(UndoOrder);
            ToAddEmployeeCommand = new DelegateCommand(ToAddEmployee);
            ToOrderEmployeesCommand = new DelegateCommand(ToOrderEmployees);
        }

        #endregion

        #region Properties

        // Order properties
        public Order OriginOfSelectedOrder
        {
            get => _originalSelectedOrder;
            set => SetProperty(ref _originalSelectedOrder, value);
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }

        public ObservableCollection<State> States { get; set; }

        public ObservableCollection<Priority> Priorities { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

        #endregion

        #region Commands

        public DelegateCommand UpdateOrderCommand { get; }
        public DelegateCommand UndoOrderCommand { get; }
        public DelegateCommand ToAddEmployeeCommand { get; }
        public DelegateCommand ToOrderEmployeesCommand { get; }

        #endregion

        #region Methods

        // Navigation methods
        private void ToAddEmployee()
        {
            regionManager.RequestNavigate("Admin.OrderDetails.Employees", "OrderAddEmployeeView");
        }

        private void ToOrderEmployees()
        {
            regionManager.RequestNavigate("Admin.OrderDetails.Employees", "OrderEmployeesView");
        }

        // CRUD methods
        private async void UpdateOrder()
        {
            SelectedOrder.Validate();
            if (SelectedOrder.HasErrors)
                return;

            OriginOfSelectedOrder.GetInfoFrom(SelectedOrder);

            await Task.Run(() => repository.UpdateOrder(OriginOfSelectedOrder));
        }

        private void UndoOrder()
        {
            SelectedOrder = OriginOfSelectedOrder.Clone();
        }

        // Event handlers
        private void OrderSelectionChangedEventHandler(Order parameter)
        {
            OriginOfSelectedOrder = parameter;
            SelectedOrder = parameter.Clone();
          
            var orderEmployees = repository.Performers
                .Where(x => x.OrderID == SelectedOrder.OrderID)
                .Select(x => x.Employee);

            Employees.Clear();
            Employees.AddRange(orderEmployees);
            SelectedEmployee = Employees.FirstOrDefault();
        }

        private void NeedRefreshListsEventHandler()
        {
            var orderEmployees = repository.Performers
                .Where(x => x.OrderID == SelectedOrder.OrderID)
                .Select(x => x.Employee);

            Employees.Clear();
            Employees.AddRange(orderEmployees);
            SelectedEmployee = Employees.FirstOrDefault();
        }

        #endregion
    }
}