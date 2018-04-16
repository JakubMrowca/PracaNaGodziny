using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Works.Models.Domain;


namespace Works.Models.Storage
{
    public class WorkDbContext: DbContext
    {
    public WorkDbContext(DbContextOptions<WorkDbContext> options)
        : base(options)
    {
#if DEBUG
        Database.Migrate();
#endif
    }

    public DbSet<Worker> Workers { get; set; }
    public DbSet<Emplyer> Employers { get; set; }
}
}
