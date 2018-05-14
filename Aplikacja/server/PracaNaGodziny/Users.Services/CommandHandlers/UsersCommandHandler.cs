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
using Users.Shared.ValueObjects;
using Works.Shared.Commands;
using Infrastructure.Domain.Queries;
using Works.Shared.Queries;
using Users.Services.Services;

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
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;
        private readonly ILoggedUserService loggedUserService;
        private DbSet<User> Users;

        public UsersCommandHandler(UsersDbContext dbContext, IEventBus eventBus, ICommandBus commandBus, IQueryBus queryBus, ILoggedUserService loggedUserService)
        {
            this.loggedUserService = loggedUserService;
            this.dbContext = dbContext;
            this.queryBus = queryBus;
            Users = dbContext.Users;
            this.eventBus = eventBus;
            this.commandBus = commandBus;
        }

        public async Task Handle(CreateUser command, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Users.AddAsync(new User(
                command.Id,
                command.Password,
                command.Email
            ));

            await dbContext.SaveChangesAsync(cancellationToken);
            await eventBus.Publish(new UserCreated(command.Id, new UserInfo{Email = command.Email,Password = command.Password}));

            if (command.IsEmployer)
            {
                var createEmplyerCommand = new CreateEmployer(Guid.NewGuid(), command.Id, new Works.Shared.ValueObjects.EmplyerInfo { FirstName = command.FirstName, LastName = command.LastName, Address = command.Address });
                await commandBus.Send(createEmplyerCommand);
            }
            if (command.IsWorker)
            {
                //var createWorkerCommand = new CreateWorker()
            }
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
            var user = await Users
                .Where(x => x.Arch == false)
                .Where(x => x.Email == command.Email && x.Password == command.Password)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new NotImplementedException();

            var userVm = await queryBus.Send<GetForUser, Works.Shared.ValueObjects.UserVm>(new GetForUser { UserId = user.Id });
            loggedUserService.SetLoggedUser(userVm);

        }
    }
}
