using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using demo.Tasks;
using System;
using static demo.Tasks.Task;

namespace demo.TaskAppService.Dto
{
    [AutoMapFrom(typeof(Task))]
    public class TaskListDto: EntityDto<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

    }
}
