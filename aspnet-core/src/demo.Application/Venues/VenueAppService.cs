using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using demo.Events.Dtos;
using Microsoft.EntityFrameworkCore;

namespace demo.Venues
{
    public class VenueAppService: demoAppServiceBase, IVenueAppService
    {
        private readonly IRepository<Venue, Guid> _venueRepository; 
        private readonly IGuidGenerator _guidGenerator;
        private readonly VenueManager _venueManager;

        public VenueAppService(IRepository<Venue, Guid> venueRepository, IGuidGenerator guidGenerator,
            VenueManager venueManager)
        {
            _venueRepository = venueRepository;
            _guidGenerator = guidGenerator;
            _venueManager = venueManager;
        }

        public async Task<ListResultDto<VenueDto>> GetListAsync(GetVenueListInput input)
        {
            var venues = await _venueRepository
                .GetAll()
                .WhereIf(input.OnlyShowAvailableVenues, v => !v.IsBooked)
                .ToListAsync();

            return new ListResultDto<VenueDto>(ObjectMapper.Map<List<VenueDto>>(venues));
        }

        public async Task CreateAsync(CreateVenueInput input)
        {
            var id = _guidGenerator.Create();

            var venue = Venue.CreateNew(id, AbpSession.GetTenantId(), input.Name, input.Address, input.City,
                input.State, false);

            await _venueManager.Create(venue);
        }
    }
}