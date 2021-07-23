using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using demo.Authorization.Roles;
using demo.Authorization.Users;
using demo.MultiTenancy;
using demo.Tasks;
using demo.Events;
using demo.Venues;

namespace demo.EntityFrameworkCore
{
    public class demoDbContext : AbpZeroDbContext<Tenant, Role, User, demoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }
        
        public DbSet<Venue> Venues { get; set; }

        public demoDbContext(DbContextOptions<demoDbContext> options)
            : base(options)
        {
        }

        
    }

}
