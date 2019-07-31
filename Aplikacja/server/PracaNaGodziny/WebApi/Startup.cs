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
using Notifications.Services;
using Notifications.Services.Services;
using Notifications.Shared.Events;
using Notifications.Shared.Hubs;
using Users.Models.Storage;
using Users.Models.View;
using Users.Services.CommandHandlers;
using Users.Services.EventHandlers;
using Users.Services.QueryHandlers;
using Users.Services.Services;
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
using Works.Services.Services;
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
            ConfigureCors(services);
            ConfigureMvc(services);
            services.AddSignalR();

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

            app.UseCors("MyPolicy");
            app.UseMvc();
            app.UseSignalR(routes =>
            {
                
                routes.MapHub<UserHub>("/userHub");
            });
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
                builder.AllowAnyHeader()
                    .SetIsOriginAllowed(host => true)
                    .AllowAnyMethod()
                    .AllowCredentials()));
        }

        private void ConfigureMvc(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddMvc();
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
                   options.Events.AddEventType(typeof(RateChanged));

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
            services.AddTransient<ServiceFactory>(p => p.GetService);

        }
        private static void ConfigureCQRS(IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();
            services.AddSingleton<ILoggedUsersMock, LoggedUsersMock>();
            services.AddScoped<IConvertPhoto, ConvertPhoto>();
            services.AddSingleton<IActiveConnections, ActiveConnections>();

            services.AddScoped<IRequestHandler<CreateNewWork, Unit>, CreateWorkHandler>();
            services.AddScoped<IRequestHandler<AddHours, Unit>, HoursHandler>();
            services.AddScoped<IRequestHandler<SubstractHours, Unit>, HoursHandler>();
            services.AddScoped<IRequestHandler<GetWork, WorkSummaryVm>, GetWorkQueryHandler>();
            services.AddScoped<IRequestHandler<GetWorkByLocationAndWorker, WorkSummaryVm>, GetWorkQueryHandler>();

            //Users
            services.AddScoped<IRequestHandler<CreateUser, Unit>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUsers, Unit>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUser, Unit>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<AuthorizeUser, Unit>, UsersCommandHandler>();

            services.AddScoped<INotificationHandler<UserCreated>, UsersEventHandler>();
            services.AddScoped<INotificationHandler<UserUpdated>, UsersEventHandler>();
            services.AddScoped<INotificationHandler<UserDeleted>, UsersEventHandler>();

            services.AddScoped<IRequestHandler<GetUser, Users.Shared.ValueObjects.UserVm>, UsersQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsers, List<Users.Shared.ValueObjects.UserVm>>, UsersQueryHandler>();

            //Notidications
            services.AddScoped<INotificationHandler<UserAuthorized>, UserAuthorizeEventHandler>();

            //Clients
            services.AddScoped<IRequestHandler<CreateLocation, Unit>, CreateLocationCommandHandler>();
            services.AddScoped<IRequestHandler<CreateClient, Unit>, CreateClientCommandHandler>();

            services.AddScoped<INotificationHandler<ClientCreated>, ClientCreatedEventHandler>();
            services.AddScoped<INotificationHandler<LocationCreated>, LocationCreatedEventHandler>();

            services.AddScoped<IRequestHandler<GetClient, Clients.Shared.ValueObjects.ClientVm>, GetClientQueryHandler>();
            services.AddScoped<IRequestHandler<GetLocation, Clients.Shared.ValueObjects.LocationVm>, GetLocationQueryHandler>();
            
            
            //Work
            services.AddScoped<IRequestHandler<CreateWorker, Unit>, CreateWorkerHandler>();
            services.AddScoped<IRequestHandler<CreateEmployer, Unit>, CreateEmplyerHandler>();
            services.AddScoped<IRequestHandler<AddWorkerForEmployer, Unit>, CreateWorkerHandler>();

            services.AddScoped<INotificationHandler<WorkerCreated>, WorkerCreatedEventHandler>();
            services.AddScoped<INotificationHandler<EmplyerCreated>, EmployerCreatedEventHandler>();
            services.AddScoped<INotificationHandler<RateChanged>, RateEventHandlers>();


            services.AddScoped<IRequestHandler<GetWorker, WorkerVm>, GetWorkerQueryHandler>();
            services.AddScoped<IRequestHandler<GetWorkersForEmployer, List<WorkerVm>>, GetWorkersForEmployerQueryHandler>();
            services.AddScoped<IRequestHandler<GetEmplyer, EmployerVm>, GetEmployerQueryHandler>();
            services.AddScoped<IRequestHandler<GetWorksForWorker, List<WorkSummaryVm>>, GetWorksForWorkerQueryHandler>();
            services.AddScoped<IRequestHandler<GetForUser, Works.Shared.ValueObjects.UserVm>, GetForUserQueryHandler>();
            services.AddScoped<IRequestHandler<AddPhotoForEmployer, Unit>, AddPhotoHandler>();
            services.AddScoped<IRequestHandler<AddWorkCommand, Unit>, AddWorkHandler>();
            services.AddScoped<IRequestHandler<GetWorksForLocation, List<WorkSummaryVm>>, GetWorksForLocationQueryHandler>();
            services.AddScoped<IRequestHandler<GetLocationsForEmployer, List<Works.Shared.ValueObjects.LocationVm>>, GetLocationsForEmployerQueryHandler>();

        }
    }
}
