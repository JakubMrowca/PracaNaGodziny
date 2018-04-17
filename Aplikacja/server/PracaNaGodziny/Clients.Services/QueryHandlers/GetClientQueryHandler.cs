using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clients.Models.Domain;
using Clients.Models.Storage;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Shared.Queries;

namespace Clients.Services.QueryHandlers
{
    public class GetClientQueryHandler: IQueryHandler<GetClient, ClientVm>
    {
        private readonly IQueryable<Employer> _employers;
        private readonly IQueryable<Client> _clients;
        private readonly IQueryable<Location> _locations;

        public GetClientQueryHandler(ClientsDbContext clientsDbContext, WorkDbContext workDbContext)
        {
            
            _clients = clientsDbContext.Clients;
            _locations = clientsDbContext.Locations;
            _employers = workDbContext.Employers;
        }

        public async Task<ClientVm> Handle(GetClient message, CancellationToken cancellationToken = default(CancellationToken))
        {
            var locationForClient = await _locations
                .Where(x => x.Arch == false && x.ClientId == message.Id)
                .ToListAsync(cancellationToken);

            var client = await _clients
                .Where(x => x.Arch == false && x.Id == message.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var employer = await _employers
                .Where(x => x.Arch == false && x.Id == client.EmployerId)
                .FirstOrDefaultAsync(cancellationToken);

            return new ClientVm
            {
                Id = client.Id,
                Address = client.Address,
                EmployerId = employer.Id,
                Email = client.Email,
                EmployerName = $"{employer.FirstName} {employer.LastName}",
                FirstName = client.FirstName,
                LastName = client.LastName,
                Locations = MapLocationToVm(locationForClient)

            };
        }

        private List<LocationVm> MapLocationToVm(IReadOnlyList<Location> locations)
        {
            return locations.Select(x => new LocationVm
            {
                Address = x.Address,
                ClientId = x.ClientId,
                Name = x.Name
               
            }).ToList();
        }
    }
}
