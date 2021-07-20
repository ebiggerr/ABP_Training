using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace demo.Events.Dtos
{
    public class CreateEventInput: EntityDto<Guid>
    {
        [Required]
        [StringLength(Event.MAX_TITLE_LENGTH)]
        public string Title { get; set; }

        [StringLength(Event.MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Range(0, int.MaxValue)]
        public int MaxRegistrationCount { get; set; }
    }
}