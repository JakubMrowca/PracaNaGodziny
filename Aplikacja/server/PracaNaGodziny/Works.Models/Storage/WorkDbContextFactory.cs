using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Works.Models.Storage
{
    public class WorkDbContextFactory: IDesignTimeDbContextFactory<WorkDbContext>
    {
        public WorkDbContextFactory()
        {
        }

        public WorkDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkDbContext>();

            if (optionsBuilder.IsConfigured)
                return new WorkDbContext(optionsBuilder.Options);

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

            return new WorkDbContext(optionsBuilder.Options);
        }
    }
}
