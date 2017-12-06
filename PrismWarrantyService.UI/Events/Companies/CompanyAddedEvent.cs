using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events.Companies
{
    public class CompanyAddedEvent : PubSubEvent<Company> { }
}