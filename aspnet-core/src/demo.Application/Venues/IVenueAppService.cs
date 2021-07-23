using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using demo.Events.Dtos;

namespace demo.Venues
{
    public interface IVenueAppService
    {
        public Task<ListResultDto<VenueDto>> GetListAsync(GetVenueListInput input);

        public Task CreateAsync(CreateVenueInput input);
    }
}