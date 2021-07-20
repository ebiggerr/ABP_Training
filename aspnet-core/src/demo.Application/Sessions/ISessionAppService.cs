using System.Threading.Tasks;
using Abp.Application.Services;
using demo.Sessions.Dto;

namespace demo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
