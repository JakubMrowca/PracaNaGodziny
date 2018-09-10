using System;
using System.Collections.Generic;
using System.Text;
using Clients.Models.Domain;
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
    }
}
