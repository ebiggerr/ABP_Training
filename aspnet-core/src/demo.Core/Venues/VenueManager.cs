using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace demo.Venues
{
    public class VenueManager: IVenueManager
    {
        private readonly IRepository<Venue, Guid> _venueRepository;

        public VenueManager(IRepository<Venue, Guid> venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<Venue> GetVenueAsync(Guid id)
        {
            return await _venueRepository.GetAsync(id);
        }

        public async Task Create(Venue venue)
        {
            //TODO logic to handle duplicate of Venue Title or Address
            
            await _venueRepository.InsertAsync(venue);
        }
    }
}