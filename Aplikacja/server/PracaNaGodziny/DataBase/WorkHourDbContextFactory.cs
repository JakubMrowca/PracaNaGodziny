using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataBase
{
    public class WorkHourDbContextFactory : IDesignTimeDbContextFactory<WorkHourDbContext>
    {
        public WorkHourDbContextFactory()
        {
        }

        public WorkHourDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkHourDbContext>();

            if (optionsBuilder.IsConfigured)
                return new WorkHourDbContext(optionsBuilder.Options);

            //Called by parameterless ctor Usually Migrations
            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

            var connectionString =
                new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory + "../../../../WebApi")
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables()
                    .Build()
                    .GetConnectionString("WorkHour");

            optionsBuilder.UseNpgsql(connectionString);

            return new WorkHourDbContext(optionsBuilder.Options);
        }
    }
}
