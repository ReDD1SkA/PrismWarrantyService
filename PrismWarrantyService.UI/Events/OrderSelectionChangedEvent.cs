﻿using Prism.Events;
using PrismWarrantyService.Domain.Entities;

namespace PrismWarrantyService.UI.Events
{
    public class OrderSelectionChangedEvent : PubSubEvent<Order> { }
}
