using Clients.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using Clients.Models.Domain.Ref;
using Users.Models.Domain;
using Works.Models.Domain;
using Works.Models.Domain.Ref;

namespace DataBase
{
    public class WorkHourDbContext:DbContext
    {
        public WorkHourDbContext(DbContextOptions<WorkHourDbContext> options)
            : base(options)
        {
#if DEBUG
            Database.Migrate();
#endif
        }

        //Works
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Worker> Workers { get; set; }

        //Users
        public DbSet<User> Users { get; set; }

        //Notifications

        //Clients
        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<LocationRef>();
            modelBuilder.Ignore<ClientRef>();
            modelBuilder.Ignore<WorkerRef>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
