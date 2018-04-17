﻿using System;
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
using Microsoft.EntityFrameworkCore;

namespace Clients.Services.CommandHandlers
{
    public class CreateLocationCommandHandler: ICommandHandler<CreateLocation>
    {
        private readonly IEventBus _eventBus;
        private ClientsDbContext _clientsDbContext;

        private DbSet<Location> _locations;
        private DbSet<Client> _clients;

        public CreateLocationCommandHandler(ClientsDbContext locationDbContext, IEventBus eventBus)
        {
            _eventBus = eventBus;
            InitContext(locationDbContext);
        }

        private void InitContext(ClientsDbContext locationDbContext)
        {
            _clientsDbContext = locationDbContext;
            _locations = _clientsDbContext.Locations;
            _clients = _clientsDbContext.Clients;
        }

        public async Task Handle(CreateLocation command, CancellationToken cancellationToken = default(CancellationToken))
        {
            Guid clientId;
            if (command.ClientId.HasValue)
            {
                var client = await _clients.FindAsync(command.ClientId);
                clientId = client.Id;
            }
                

            await _locations.AddAsync(new Location(
                command.Id,             
                command.Data.Name, 
                command.Data.Address,
                clientId
            ));

            await _clientsDbContext.SaveChangesAsync(cancellationToken);
            await _eventBus.Publish(new LocationCreated(command.Id, clientId, command.Data));

        }
    }
}
