using System;
using System.Collections.Generic;
using System.Text;
using Clients.Models.Domain;
using Clients.Models.Domain.Ref;
using Microsoft.EntityFrameworkCore;

namespace Clients.Models.Storage
{
    public class ClientsDbContext : DbContext
    {
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options)
            : base(options)
        {
            #if DEBUG
            Database.Migrate();
            #endif
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }

        //ref
        public DbSet<WorkerRef> Workers { get; set; }
    }
}
