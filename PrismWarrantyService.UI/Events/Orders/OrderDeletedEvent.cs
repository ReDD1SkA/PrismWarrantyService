using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events.Orders
{
    class OrderDeletedEvent : PubSubEvent<Order> { }
}
