using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Users.Models.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Users.Models.Storage
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
            #if DEBUG
            Database.Migrate();
            #endif
        }

        public DbSet<User> Users { get; set; }

    }
}
