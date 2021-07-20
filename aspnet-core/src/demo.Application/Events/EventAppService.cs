using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using demo.Authorization.Users;
using demo.Events.Dtos;
using Microsoft.EntityFrameworkCore;

namespace demo.Events
{
    [AbpAuthorize]
    //public class EventAppService : demoAppServiceBase, IEventAppService /*AsyncCrudAppService<Event,EventListDto, Guid, PagedEventResultRequestDto, CreateEventInput>, IEventAppService*/  {
    // public class EventAppService: IApplicationService{
    public class EventAppService : AsyncCrudAppService<Event,EventListDto,Guid,PagedEventResultRequestDto,CreateEventInput> {
    private readonly IEventManager _eventManager;
        private readonly IRepository<Event, Guid> _eventRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly UserManager _userManager;

        public EventAppService(IRepository<Event, Guid> repository,
            IEventManager eventManager,
            IRepository<Event, Guid> eventRepository,
            IGuidGenerator guidGenerator,
            UserManager userManager) : base(repository)
        {
            _eventManager = eventManager;
            _eventRepository = eventRepository;
            _guidGenerator = guidGenerator;
            _userManager = userManager;
        }

        public async Task<ListResultDto<EventListDto>> GetListAsync(GetEventListInput input)
        {
            var events = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                // .Take(64)
                .ToListAsync();

            return new ListResultDto<EventListDto>(ObjectMapper.Map<List<EventListDto>>(events));
            // return new ListResultDto<EventListDto>(ObjectMapper.MapTo<List<EventListDto>>(events));
        }

        public async Task<PagedResultDto<EventListDto>> GetPagedListingAsync()
        {
            var events = await _eventRepository
                .GetAll().ToListAsync();
                //
                // .Skip(#offset)
                // .Take(#limit) //
            
            
            //paging logic

            //TODO dummy
            return new PagedResultDto<EventListDto>();
        }

        public async Task<EventDetailOutput> GetDetailAsync(EntityDto<Guid> input)
        {
            var @event = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .ThenInclude(r => r.User)
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();

            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            return ObjectMapper.Map<EventDetailOutput>(@event);
            // return ObjectMapper.MapTo<EventDetailOutput>(@event);
        }
        
        public async Task CreateEventAsync(CreateEventInput input)
        {
            var userId = AbpSession.GetUserId();
            if (userId == 1)
            {
                throw new UserFriendlyException("Host cannot create an event");
            }

            var id = _guidGenerator.Create(); 
            var @event = Event.Create(id,AbpSession.GetTenantId(), input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            
            await _eventManager.CreateAsync(@event);
        }

        public async Task CancelAsync(EntityDto<Guid> input)
        {
            var @event = await _eventManager.GetAsync(input.Id);
            _eventManager.Cancel(@event);
        }

        public async Task<EventRegisterOutput> RegisterAsync(EntityDto<Guid> input)
        {
            var registration = await RegisterAndSaveAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );

            return new EventRegisterOutput
            {
                RegistrationId = registration.Id
            };
        }

        public async Task CancelRegistrationAsync(EntityDto<Guid> input)
        {
            await _eventManager.CancelRegistrationAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );
        }

        private async Task<EventRegistration> RegisterAndSaveAsync(Event @event, User user)
        {
            var registration = await _eventManager.RegisterAsync(@event, user);
            // await CurrentUnitOfWork.SaveChangesAsync(); //TODO is the explicit call of SaveChangesAsync() necessary?
            return registration;
        }
        
        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }
    }
}
