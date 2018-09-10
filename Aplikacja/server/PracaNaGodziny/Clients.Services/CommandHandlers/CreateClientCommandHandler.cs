using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clients.Models.Domain;
using Clients.Models.Storage;
using Clients.Shared.Commands;
using Clients.Shared.Events;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Shared.Events;

namespace Clients.Services.CommandHandlers
{
    public class CreateClientCommandHandler : ICommandHandler<CreateClient>
    {
        private readonly IEventBus _eventBus;
        private ClientsDbContext _clientsDbContext;

        private DbSet<Employer> _emplyers;
        private DbSet<Client> _clients;

        public CreateClientCommandHandler(ClientsDbContext locationDbContext, WorkDbContext workDbContext, IEventBus eventBus)
        {
            _eventBus = eventBus;
            InitContext(locationDbContext, workDbContext);
        }

        private void InitContext(ClientsDbContext locationDbContext, WorkDbContext workDbContext)
        {
            _clientsDbContext = locationDbContext;
            _emplyers = workDbContext.Employers;
            _clients = _clientsDbContext.Clients;
        }

        public async Task<Unit> Handle(CreateClient command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var employer = await _emplyers.FindAsync(command.EmployerId);

            await _clients.AddAsync(new Client(
                command.Id,
                employer.Id,
                command.Data.FirstName,
                command.Data.LastName,
                command.Data.Address,
                command.Data.Email,
                command.Data.Phone
            ));

            await _clientsDbContext.SaveChangesAsync(cancellationToken);
            await _eventBus.Publish(new ClientCreated(command.Id, employer.Id, command.Data));
            return Unit.Value;

        }
    }
}
