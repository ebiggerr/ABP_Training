using System;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;

namespace demo.Venues
{
    public class VenueBookedEvent: EventData
    {
        public Guid VenueId { get; set; }
        
    }
}