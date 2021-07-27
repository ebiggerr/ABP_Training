using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace demo.Venues
{
    public class VenueManager: IVenueManager
    {
        private readonly IRepository<Venue, Guid> _venueRepository;

        public VenueManager(IRepository<Venue, Guid> venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<Venue> CheckVenueAsync(Guid id)
        {
            var venue =  await _venueRepository.GetAsync(id);

            if (venue == null)
            {
                throw new UserFriendlyException("Venue Not Found");
            }

            if (venue.IsBooked)
            {
                throw new UserFriendlyException("Venue is already booked by other event.");
            }
            
            venue.MakeItBooked();

            return venue;
        }

        public async Task Create(Venue venue)
        {
            //TODO logic to handle duplicate of Venue Title or Address
            
            await _venueRepository.InsertAsync(venue);
        }
        
    }
}