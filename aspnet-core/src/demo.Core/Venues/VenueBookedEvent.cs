using Abp.Events.Bus.Entities;

namespace demo.Venues
{
    public class VenueBookedEvent: EntityEventData<Venue>
    {
        public VenueBookedEvent(Venue entity) : base(entity)
        {
        }
    }
}