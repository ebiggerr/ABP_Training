﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.UI;
using demo.Authorization.Users;
using demo.Venues;
using Microsoft.EntityFrameworkCore;

namespace demo.Events
{
    public class EventManager : IEventManager
    {
        public IEventBus EventBus { get; set; }

        private readonly IEventRegistrationPolicy _registrationPolicy;
        private readonly IRepository<EventRegistration> _eventRegistrationRepository;
        private readonly IRepository<Event, Guid> _eventRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<Venue, Guid> _venueRepository;

        public EventManager(
            IEventRegistrationPolicy registrationPolicy,
            IRepository<EventRegistration> eventRegistrationRepository,
            IRepository<Event, Guid> eventRepository, 
            IRepository<Venue, Guid> venueRepository,
            IGuidGenerator guidGenerator)
        {
            _registrationPolicy = registrationPolicy;
            _eventRegistrationRepository = eventRegistrationRepository;
            _eventRepository = eventRepository;
            _venueRepository = venueRepository;
            _guidGenerator = guidGenerator;

            EventBus = NullEventBus.Instance;
        }

        public async Task<Event> GetAsync(Guid id)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(id);
            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted!");
            }

            return @event;
        }
        
        public async Task CreateAsync(Event @event)
        {
            EventBus.Trigger(new VenueBookedEvent
            {
                VenueId = @event.VenueId,
            });
            
            await _eventRepository.InsertAsync(@event);
        }

        public void Cancel(Event @event)
        {
            var venue = _venueRepository.Get(@event.VenueId);
            @event.Cancel();
            venue.Unbook();
            EventBus.Trigger(new EventCancelledEvent(@event));
            EventBus.Trigger(new VenueBookedEvent()); //release the booked venue to open booking for upcoming event
        }

        public async Task<EventRegistration> RegisterAsync(Event @event, User user)
        {
            return await _eventRegistrationRepository.InsertAsync(
                await EventRegistration.CreateAsync(@event, user, _registrationPolicy)
                );
        }

        public async Task CancelRegistrationAsync(Event @event, User user)
        {
            var registration = await _eventRegistrationRepository.FirstOrDefaultAsync(r => r.EventId == @event.Id && r.UserId == user.Id);
            if (registration == null)
            {
                //No need to cancel since there is no such a registration
                return;
            }

            await registration.CancelAsync(_eventRegistrationRepository);
        }

        public async Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Event @event)
        {
            return await _eventRegistrationRepository
                .GetAll()
                .Include(registration => registration.User)
                .Where(registration => registration.EventId == @event.Id)
                .Select(registration => registration.User)
                .ToListAsync();
        }
    }
}