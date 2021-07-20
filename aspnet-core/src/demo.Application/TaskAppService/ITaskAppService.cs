using Abp.Application.Services;
using Abp.Application.Services.Dto;
using demo.TaskAppService.Dto;
using System.Threading.Tasks;

namespace demo.TaskAppService
{
    public interface ITaskAppService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll();

        Task<Tasks.Task> AddNewTask(RequestDto requestDto);

        Task<Tasks.Task> UpdateTask(TaskListDto taskListDto);

        void DeleteTask(int id);

    }
}
