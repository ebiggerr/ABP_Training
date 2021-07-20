using System.Threading.Tasks;
using Abp.Application.Services;
using demo.Authorization.Accounts.Dto;

namespace demo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
