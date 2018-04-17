using Infrastructure.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clients.Models.Domain;
using Clients.Models.Storage;
using Clients.Shared.Queries;
using Clients.Shared.ValueObjects;
using Works.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace Clients.Services.QueryHandlers
{
    public class GetLocationQueryHandler : IQueryHandler<GetLocation, LocationVm>
    {
        private readonly IQueryable<Location> _locations;
        private readonly IQueryable<Client> _clients;

        public GetLocationQueryHandler(ClientsDbContext clientsDbContext)
        {
            _clients = clientsDbContext.Clients;
            _locations = clientsDbContext.Locations;
        }

        public async Task<LocationVm> Handle(GetLocation message, CancellationToken cancellationToken = default(CancellationToken))
        {
            var location = await _locations
                .Where(x => x.Arch == false && x.Id == message.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var client = await _clients
                .Where(x => x.Arch == false && x.Id == location.ClientId)
                .FirstOrDefaultAsync(cancellationToken);

            return new LocationVm
            {
                Id = location.Id,
                Address = location.Address,
                ClientId = client.Id,
                Name = location.Name,
                ClientName = $"{client.FirstName} {client.LastName}",
                Client = new ClientVm
                {
                    Address = client.Address,
                    Id = client.Id,
                    Email = client.Address,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Phone = client.Phone,
                    EmployerId = client.EmployerId
                }
             
            };
        }
    }
}
