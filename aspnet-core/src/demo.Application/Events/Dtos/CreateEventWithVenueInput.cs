using System;

namespace demo.Events.Dtos
{
    public class CreateEventWithVenueInput: CreateEventInput
    {
        public Guid venueId { get; set; }
    }
}