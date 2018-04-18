using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Events;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Users.Models.Domain;
using Users.Models.Storage;
using Users.Shared.Commands;
using Users.Shared.Events;

namespace Users.Services.CommandHandlers
{
    public class UsersCommandHandler :
        ICommandHandler<CreateUser>,
        ICommandHandler<UpdateUsers>,
        ICommandHandler<DeleteUser>,
        ICommandHandler<AuthorizeUser>
    {

        private readonly UsersDbContext dbContext;
        private readonly IEventBus eventBus;

        private DbSet<User> Users;

        public UsersCommandHandler(UsersDbContext dbContext, IEventBus eventBus)
        {
            this.dbContext = dbContext;
            Users = dbContext.Users;
            this.eventBus = eventBus;
        }

        public async Task Handle(CreateUser command, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Users.AddAsync(new User(
                command.Id,
                command.Data.Login,
                command.Data.Password,
                command.Data.Email
            ));

            await dbContext.SaveChangesAsync(cancellationToken);
            await eventBus.Publish(new UserCreated(command.Id, command.Data));
        }

        public async Task Handle(UpdateUsers command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var users = await Users.FindAsync(command.Id);

            users.Update(command.Data);
            dbContext.Update(users);

            await dbContext.SaveChangesAsync(cancellationToken);
            await eventBus.Publish(new UserUpdated(command.Id, command.Data));

        }

        public async Task Handle(DeleteUser command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var users = await Users.FindAsync(command.Id);

            users.Delete();
            dbContext.Update(users);

            await dbContext.SaveChangesAsync(cancellationToken);
            await eventBus.Publish(new UserDeleted(command.Id));
        }

        public async Task Handle(AuthorizeUser command, CancellationToken cancellationToken)
        {
            var user = Users
                .Where(x => x.Arch == false)
                .Where(x => x.Login == command.Login && x.Password == command.Password)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
                throw new NotImplementedException();
        }
    }
}
