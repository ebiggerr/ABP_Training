using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using demo.TaskAppService.Dto;
using Microsoft.EntityFrameworkCore;

namespace demo.TaskAppService
{
    public class TaskAppService : demoAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Tasks.Task> _taskRepository;

        public TaskAppService(IRepository<Tasks.Task> taskRepository) {
            _taskRepository = taskRepository;
        }

        public async Task<ListResultDto<TaskListDto>> GetAll()
        {
            var tasks = await _taskRepository.GetAll()
            //.WhereIf(input.State.HasValue, t => t.State == input.State.Value)
            .OrderByDescending(t => t.CreationTime)
            .ToListAsync();

            //var newList = new List<TaskListDto>();

            return new ListResultDto<TaskListDto>(
               ObjectMapper.Map<List<TaskListDto>>(tasks)
            );
           
        }

        public async Task<Tasks.Task> AddNewTask(RequestDto requestDto)
        { 
            var newTaskItem = await _taskRepository.InsertAsync( new Tasks.Task( requestDto.Title, requestDto.Description ) );

            return newTaskItem;
            
        }

        public async Task<Tasks.Task> UpdateTask(TaskListDto taskListDto)
        {
            try
            {
                var task = await _taskRepository.SingleAsync(t => t.Id.Equals(taskListDto.Id)); 

                task.Title = taskListDto.Title;
                task.Description = taskListDto.Description;

                var update = await _taskRepository.UpdateAsync(task);

                return task;

            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public void DeleteTask(int id)
        {
            var delete = _taskRepository.DeleteAsync(id);
        }


    }
}
