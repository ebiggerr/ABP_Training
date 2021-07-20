using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace demo.Tasks
{
    //mapping to the table in the database
    [Table("AppTasks")]
    public class Task : Entity, IHasCreationTime
    {
        private const int MAX_TITLE_LENGTH = 256;
        private const int MAX_DESCRIPTION_LENGTH = 64 * 1024;

        //not null
        //validation
        [Required]
        [StringLength(MAX_TITLE_LENGTH)]
        public string Title { get; set; }

        [StringLength(MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        public Task()
        {
            // Clock.Now is preffered while working with ABP framework rather than DateTime.UtcNow
            CreationTime = Clock.Now;
            State = TaskState.Open;
        }

        public Task(string title, string description = null)
            : this()
        {
            Title = title;
            Description = description;
        }
        public enum TaskState : byte
        {
            Open = 0,
            Completed = 1
        }
    }
}
