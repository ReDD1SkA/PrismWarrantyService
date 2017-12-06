using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events.Orders
{
    public class OrderSelectionChangedEvent : PubSubEvent<Order> { }
}