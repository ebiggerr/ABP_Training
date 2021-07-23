using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace demo.Venues
{
    [Table(("AppVenues"))]
    public class Venue: FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        [Required] 
        public virtual string Name { get; protected set; }
        
        public virtual string Address { get; protected set; }
        
        public virtual string City { get; protected set; }
        
        public virtual string State { get; protected set; }

        public virtual bool IsBooked { get; protected set; }

        protected Venue()
        {
        }

        public static Venue CreateEmpty()
        {
            return new Venue();
        }

        public static Venue CreateNew(Guid guid, int tenantId, string name, string address, string city, string state, bool isBooked)
        {

            var venue = new Venue
            {
                Id = guid,
                TenantId = tenantId,
                Name = name,
                Address = address,
                City = city,
                State = state,
                IsBooked = isBooked
            };

            return venue;
        }

        public bool IsAvailable()
        {
            return !IsBooked;
        }

        public void MakeItBooked()
        {
            IsBooked = true;
        }

        public void Unbook()
        {
            IsBooked = false;
        }
    }
}