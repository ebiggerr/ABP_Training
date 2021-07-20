using static demo.Tasks.Task;

namespace demo.TaskAppService.Dto
{
    public class GetAllTaskInput
    {
        public TaskState? State { get; set; }
    }

}
