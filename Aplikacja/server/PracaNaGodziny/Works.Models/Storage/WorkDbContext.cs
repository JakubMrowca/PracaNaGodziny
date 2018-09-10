using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Works.Models.Domain;
using Works.Models.Domain.Ref;


namespace Works.Models.Storage
{
    public class WorkDbContext : DbContext
    {
        public WorkDbContext(DbContextOptions<WorkDbContext> options)
            : base(options)
        {
#if DEBUG
            Database.Migrate();
#endif
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Employer> Employers { get; set; }

        //ref
        public DbSet<LocationRef> Locations { get; set; }
        public DbSet<ClientRef> Clients { get; set; }

    }
}
