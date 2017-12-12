using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events.Clients
{
    public class ClientCreatedEvent : PubSubEvent<Client> { }
}