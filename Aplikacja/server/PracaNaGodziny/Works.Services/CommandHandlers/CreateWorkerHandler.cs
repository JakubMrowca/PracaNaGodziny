using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Users.Models.Domain;
using Users.Models.Storage;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Shared.Commands;
using Works.Shared.Events;

namespace Works.Services.CommandHandlers
{
    public class CreateWorkerHandler : ICommandHandler<CreateWorker>
    {
        private readonly IEventBus _eventBus;
        private WorkDbContext _workDbContext;

        private  DbSet<Worker> _workers;
        private  DbSet<User> _users;
        private  DbSet<Emplyer> _emplyers;

        public CreateWorkerHandler(WorkDbContext workDbContext, UsersDbContext userDbContext, IEventBus eventBus)
        {
            _eventBus = eventBus;
            InitContext(workDbContext, userDbContext);
        }

        private void InitContext(WorkDbContext workDbContext, UsersDbContext userDbContext)
        {
            _workDbContext = workDbContext;
            _workers = workDbContext.Workers;
            _emplyers = workDbContext.Employers;
            _users = userDbContext.Users;
        }

        public async Task Handle(CreateWorker command, CancellationToken cancellationToken = default(CancellationToken))
        {
            Guid? userId = null;
            if (command.UserId.HasValue)
            {
                var user = await _users.FindAsync(command.UserId);
                userId = user?.Id;
            }

            var emplyer = await _users.FindAsync(command.EmployerId);

            await _workers.AddAsync(new Worker(
                command.Id,
                userId,
                emplyer.Id,
                command.Data.FirstName,
                command.Data.LastName,
                command.Data.Address,
                command.Data.AccountNumber
            ));

            await _workDbContext.SaveChangesAsync(cancellationToken);
            await _eventBus.Publish(new WorkerCreated(command.Id, userId, emplyer.Id, command.Data));
        }
    }
}
