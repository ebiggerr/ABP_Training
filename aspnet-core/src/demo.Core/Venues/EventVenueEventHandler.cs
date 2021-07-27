using System;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus.Handlers;
using Abp.Threading;
using demo.Events;

namespace demo.Venues
{
    public class EventVenueEventHandler : AbpServiceBase, IEventHandler<VenueBookedEvent>, ITransientDependency
    {
        private readonly VenueManager _venueManager;
        private readonly EventManager _eventManager;

        public EventVenueEventHandler(VenueManager venueManager,EventManager eventManager)
        {
            _venueManager = venueManager;
            _eventManager = eventManager;
        }

        [UnitOfWork]
        public virtual void HandleEvent(VenueBookedEvent eventData)
        {
            AsyncHelper.RunSync(async () => await HandleBookingToAVenue(eventData.VenueId));
        }

        private async Task HandleBookingToAVenue(Guid eventDataVenueId)
        {
            await _venueManager.CheckVenueAsync(eventDataVenueId);
        }
    }
}