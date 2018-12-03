using Infrastructure.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clients.Models.Domain;
using Clients.Models.Domain.Ref;
using Clients.Models.Storage;
using Clients.Shared.Queries;
using Works.Shared.Queries;
using Microsoft.EntityFrameworkCore;
using Works.Shared.ValueObjects;
using ClientVm = Clients.Shared.ValueObjects.ClientVm;
using LocationVm = Clients.Shared.ValueObjects.LocationVm;

namespace Clients.Services.QueryHandlers
{
    public class GetLocationQueryHandler : IQueryHandler<GetLocation, LocationVm>
    {
        private readonly IQueryable<Location> _locations;
        private readonly IQueryable<Client> _clients;
        private readonly IQueryable<WorkerRef> _workers;

        private readonly IQueryBus _queryBus;

        public GetLocationQueryHandler(ClientsDbContext clientsDbContext, IQueryBus queryBus)
        {
            _clients = clientsDbContext.Clients;
            _workers = clientsDbContext.Workers;
            _queryBus = queryBus;
            _locations = clientsDbContext.Locations;
        }

        public async Task<LocationVm> Handle(GetLocation message, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {

            var location = await _locations
                .Where(x => x.Arch == false && x.Id == message.Id)
                .FirstOrDefaultAsync(cancellationToken);

                if (location == null)
                {
                    throw new Exception();
                }
            var client = await _clients
                .Where(x => x.Arch == false && x.Id == location.ClientId)
                .FirstOrDefaultAsync(cancellationToken);

            var worksForLocation = await _queryBus.Send<GetWorksForLocation, List<WorkSummaryVm>>
                (new GetWorksForLocation(location.Id, message.From, message.To));

                var workersVm = worksForLocation.GroupBy(x => x.WorkerId).Select(x => new WorkerVm
                {
                    Id = x.Key,
                    Wage = x.Sum(y => (y.Wage))
                }).ToList();

                foreach (var worker in workersVm)
                {
                    var workerEntity = await _workers.Where(x => x.Id == worker.Id).FirstOrDefaultAsync(cancellationToken);
                    worker.FirstName = workerEntity.FirstName;
                    worker.LastName = workerEntity.LastName;
                }

                return new LocationVm
            {
                Id = location.Id,
                Address = location.Address,
                ClientId = client?.Id,
                Name = location.Name,
                ClientName = $"{client?.FirstName} {client?.LastName}",
                Client = client == null ? null : new ClientVm
                {
                    Address = client.Address,
                    Id = client.Id,
                    Email = client.Address,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Phone = client.Phone,
                    EmployerId = client.EmployerId
                },
                PaidHour = worksForLocation.Sum(x => x.PaidHour),
                TotalHour = worksForLocation.Sum(x => x.UnpaidHour),
                Wage = worksForLocation.Sum(x => x.Wage),
                TotalWage = worksForLocation.Sum(x=> x.TotalWage),
                TotalHourInThisMonth = worksForLocation.Sum(x => x.TotalHourInThisMonth),
                PaidHourInThisMonth = worksForLocation.Sum(x => x.PaidHourInThisMonth),
                PaidHourInThisWeek = worksForLocation.Sum(x => x.PaidHourInThisWeek),
                TotalHourInThisWeek = worksForLocation.Sum(x => x.TotalHourInThisWeek),
                TotalHourInLastMonth = worksForLocation.Sum(x => x.TotalHourInLastMonth),
                Workers = workersVm
            };
            }catch(Exception ex)
            {
                return null;
            }

        }
    }
}
