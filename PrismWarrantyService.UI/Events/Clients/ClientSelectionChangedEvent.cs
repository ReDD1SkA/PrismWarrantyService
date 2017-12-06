using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events.Clients
{
    public class ClientSelectionChangedEvent : PubSubEvent<Client> { }
}