using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace demo.Venues
{
    public interface IVenueManager: IDomainService
    {
        public Task<Venue> CheckVenueAsync(Guid id);

        public Task Create(Venue venue);
    }
}