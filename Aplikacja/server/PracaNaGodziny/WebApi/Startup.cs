using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clients.Models.Storage;
using Clients.Services.CommandHandlers;
using Clients.Services.EventHandlers;
using Clients.Services.QueryHandlers;
using Clients.Shared.Commands;
using Clients.Shared.Events;
using Clients.Shared.Queries;
using Clients.Shared.ValueObjects;
using DataBase;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Events;
using Infrastructure.Domain.Queries;
using Marten;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Users.Models.Storage;
using Users.Models.View;
using Users.Services.CommandHandlers;
using Users.Services.EventHandlers;
using Users.Services.QueryHandlers;
using Users.Shared.Commands;
using Users.Shared.Events;
using Users.Shared.Queries;
using Users.Shared.ValueObjects;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Models.View;
using Works.Services.CommandHandlers;
using Works.Services.EventHandlers;
using Works.Services.QueryHandlers;
using Works.Shared.Commands;
using Works.Shared.Events;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            ConfigureEf(services);
            ConfigureMediator(services);
            ConfigureMarten(services);
            ConfigureCQRS(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            var z = new Work();
        }

        private void ConfigureEf(IServiceCollection services)
        {
            services.AddDbContext<WorkHourDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("WorkHour")));
            services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("WorkHour")));
            services.AddDbContext<ClientsDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("WorkHour")));
            services.AddDbContext<WorkDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("WorkHour")));
        }

        private void ConfigureMarten(IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var documentStore = DocumentStore.For(options =>
                {
                    var config = Configuration.GetSection("EventStore");
                    var connectionString = config.GetValue<string>("ConnectionString");
                    var schemaName = config.GetValue<string>("Schema");

                    options.Connection(connectionString);
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                    options.Events.DatabaseSchemaName = schemaName;
                    options.DatabaseSchemaName = schemaName;

                    options.Events.InlineProjections.AggregateStreamsWith<Work>();
                    options.Events.InlineProjections.Add(new WorkSummaryViewProjection());
                    options.Events.InlineProjections.Add(new UsersViewProjection());

                    options.Events.AddEventType(typeof(NewWorkCreated));
                    options.Events.AddEventType(typeof(NewInflowRecorded));
                    options.Events.AddEventType(typeof(NewOutflowRecorded));
                    options.Events.AddEventType(typeof(WorkerCreated));
                    options.Events.AddEventType(typeof(EmplyerCreated));

                    options.Events.AddEventType(typeof(UserCreated));
                    options.Events.AddEventType(typeof(UserUpdated));
                    options.Events.AddEventType(typeof(UserDeleted));

                    options.Events.AddEventType(typeof(LocationCreated));
                    options.Events.AddEventType(typeof(ClientCreated));
                });

                return documentStore.OpenSession();
            });
        }
        private static void ConfigureMediator(IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<SingleInstanceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<MultiInstanceFactory>(sp => t => sp.GetServices(t));
        }
        private static void ConfigureCQRS(IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            services.AddScoped<IRequestHandler<CreateNewWork>, CreateWorkHandler>();
            services.AddScoped<IRequestHandler<AddHours>, HoursHandler>();
            services.AddScoped<IRequestHandler<SubstractHours>, HoursHandler>();
            services.AddScoped<IRequestHandler<GetWork, WorkSummaryVm>, GetWorkQueryHandler>();


            //Users
            services.AddScoped<IRequestHandler<CreateUser>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUsers>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUser>, UsersCommandHandler>();

            services.AddScoped<INotificationHandler<UserCreated>, UsersEventHandler>();
            services.AddScoped<INotificationHandler<UserUpdated>, UsersEventHandler>();
            services.AddScoped<INotificationHandler<UserDeleted>, UsersEventHandler>();

            services.AddScoped<IRequestHandler<GetUser, UserVm>, UsersQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsers, List<UserVm>>, UsersQueryHandler>();


            //Clients
            services.AddScoped<IRequestHandler<CreateLocation>, CreateLocationCommandHandler>();
            services.AddScoped<IRequestHandler<CreateClient>, CreateClientCommandHandler>();

            services.AddScoped<INotificationHandler<ClientCreated>, ClientCreatedEventHandler>();
            services.AddScoped<INotificationHandler<LocationCreated>, LocationCreatedEventHandler>();

            services.AddScoped<IRequestHandler<GetClient, Clients.Shared.ValueObjects.ClientVm>, GetClientQueryHandler>();
            services.AddScoped<IRequestHandler<GetLocation, Clients.Shared.ValueObjects.LocationVm>, GetLocationQueryHandler>();
            
            
            //Work
            services.AddScoped<IRequestHandler<CreateWorker>, CreateWorkerHandler>();
            services.AddScoped<IRequestHandler<CreateEmployer>, CreateEmplyerHandler>();

            services.AddScoped<INotificationHandler<WorkerCreated>, WorkerCreatedEventHandler>();
            services.AddScoped<INotificationHandler<EmplyerCreated>, EmployerCreatedEventHandler>();

            services.AddScoped<IRequestHandler<GetWorker, WorkerVm>, GetWorkerQueryHandler>();
            services.AddScoped<IRequestHandler<GetEmplyer, EmployerVm>, GetEmployerQueryHandler>();
            services.AddScoped<IRequestHandler<GetWorksForWorker, List<WorkSummaryVm>>, GetWorksForWorkerQueryHandler>();
        }
    }
}
