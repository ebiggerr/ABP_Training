using System.Threading.Tasks;
using Abp.Domain.Services;
using demo.Authorization.Users;

namespace demo.Events
{
    public interface IEventRegistrationPolicy: IDomainService
    {
        Task CheckRegistrationAttemptAsync(Event @event, User user);
    }
}