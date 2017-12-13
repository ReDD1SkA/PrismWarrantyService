using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events.Employees
{
    public class EmployeeSelectionChangedEvent : PubSubEvent<Employee> { }
}