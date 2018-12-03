using Infrastructure.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Works.Models.Domain;
using Works.Models.Domain.Ref;
using Works.Models.Storage;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.QueryHandlers
{
    public class GetLocationsForEmployerQueryHandler : IQueryHandler<GetLocationsForEmployer, List<LocationVm>>
    {
        //private readonly IQueryable<Employer> _employers;
        private readonly IQueryable<LocationRef> _locations;
        private readonly IQueryable<ClientRef> _clients;
        private readonly IQueryBus _queryBus;

        public GetLocationsForEmployerQueryHandler(WorkDbContext workDbContext, IQueryBus queryBus)
        {

            _queryBus = queryBus;
            _clients = workDbContext.Clients;
            _locations = workDbContext.Locations;
            //_locations = workDbContext.Locations;
            //_employers = workDbContext.Employers;
        }

        public async Task<List<LocationVm>> Handle(GetLocationsForEmployer message, CancellationToken cancellationToken)
        {
            var clients = await _clients.Where(x => x.Arch == false && x.EmployerId == message.EmployerId).ToListAsync();

            List<LocationRef> locations = new List<LocationRef>();

            foreach(var client in clients)
            {
                locations.AddRange(await _locations.Where(x => x.Arch == false && x.ClientId == client.Id).ToListAsync(cancellationToken));
            }
            
            locations.AddRange(await _locations.Where(x => x.Arch == false && x.EmployerId == message.EmployerId).ToListAsync());

            List<LocationVm> locationsVm = new List<LocationVm>();

            foreach (var location in locations)
            {
                var worksForLocation = await
                _queryBus.Send<GetWorksForLocation, List<WorkSummaryVm>>(new GetWorksForLocation(location.Id));

                locationsVm.Add(new LocationVm
                {
                    Address = location.Address,
                    Name = location.Name,
                    Id = location.Id,
                    PaidHour = worksForLocation.Sum(x => x.PaidHour),
                    TotalHour = worksForLocation.Sum(x => x.UnpaidHour),
                    Wage = worksForLocation.Sum(x => x.Wage),
                    TotalWage = worksForLocation.Sum(x => x.TotalWage),
                    TotalHourInThisMonth = worksForLocation.Sum(x => x.TotalHourInThisMonth),
                    PaidHourInThisMonth = worksForLocation.Sum(x => x.PaidHourInThisMonth),
                    PaidHourInThisWeek = worksForLocation.Sum(x => x.PaidHourInThisWeek),
                    TotalHourInThisWeek = worksForLocation.Sum(x => x.TotalHourInThisWeek),
                    TotalHourInLastMonth = worksForLocation.Sum(x => x.TotalHourInLastMonth),

                });
            }
            return locationsVm;
        }
    }
}
