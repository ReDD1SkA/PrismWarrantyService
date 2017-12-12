using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events.Companies
{
    public class CompanySelectionChangedEvent : PubSubEvent<Company> { }
}