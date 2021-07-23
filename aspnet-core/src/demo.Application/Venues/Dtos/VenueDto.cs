using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using demo.Venues;

namespace demo.Events.Dtos

{
    [AutoMap(typeof(Venue))]
    public class VenueDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Address { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
    }
}