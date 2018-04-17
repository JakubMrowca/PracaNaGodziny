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
    public class CreateEmplyerHandler: ICommandHandler<CreateEmployer>
    {
        private readonly IEventBus _eventBus;
        private WorkDbContext _workDbContext;

        private DbSet<User> _users;
        private DbSet<Employer> _emplyers;

        public CreateEmplyerHandler(WorkDbContext workDbContext, UsersDbContext userDbContext, IEventBus eventBus)
        {
            _eventBus = eventBus;
            InitContext(workDbContext, userDbContext);
        }

        private void InitContext(WorkDbContext workDbContext, UsersDbContext userDbContext)
        {
            _workDbContext = workDbContext;
            _emplyers = workDbContext.Employers;
            _users = userDbContext.Users;
        }

        public async Task Handle(CreateEmployer command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _users.FindAsync(command.UserId);

            await _emplyers.AddAsync(new Employer(
                command.Id, 
                user.Id, 
                command.Data.FirstName, 
                command.Data.LastName,
                command.Data.Address, 
                command.Data.AccountNumber
            ));

            await _workDbContext.SaveChangesAsync(cancellationToken);
            await _eventBus.Publish(new EmplyerCreated(command.Id, user.Id, command.Data));

        }
    }
}
