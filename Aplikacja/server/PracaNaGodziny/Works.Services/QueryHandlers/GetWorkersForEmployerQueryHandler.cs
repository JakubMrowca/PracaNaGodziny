using Infrastructure.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.QueryHandlers
{
    public class GetWorkersForEmployerQueryHandler : IQueryHandler<GetWorkersForEmployer, List<WorkerVm>>
    {
        //private readonly IQueryable<Employer> _employers;
        private readonly IQueryable<Worker> _workers;
        private readonly IQueryBus _queryBus;

        public GetWorkersForEmployerQueryHandler(WorkDbContext workDbContext, IQueryBus queryBus)
        {

            _queryBus = queryBus;
            _workers = workDbContext.Workers;
            //_locations = workDbContext.Locations;
            //_employers = workDbContext.Employers;
        }

        public async Task<List<WorkerVm>> Handle(GetWorkersForEmployer message, CancellationToken cancellationToken)
        {
            var workers = await _workers
                .Where(x => x.Arch == false && x.EmployerId == Guid.Parse(message.EmployerId))
                .ToListAsync(cancellationToken);

            var workersVm = new List<WorkerVm>();

            foreach(var worker in workers)
            {

                var worksForWorker = await
                _queryBus.Send<GetWorksForWorker, List<WorkSummaryVm>>(new GetWorksForWorker(worker.Id));

                workersVm.Add(new WorkerVm
                {
                    Address = worker.Address,
                    Works = worksForWorker,
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                    Id = worker.Id,
                    PaidHour = worksForWorker.Sum(x => x.PaidHour),
                    TotalHour = worksForWorker.Sum(x => x.UnpaidHour),
                    Wage = worksForWorker.Sum(x => x.Wage),
                    TotalHourInThisMonth = worksForWorker.Sum(x => x.TotalHourInThisMonth),
                    PaidHourInThisMonth = worksForWorker.Sum(x => x.PaidHourInThisMonth),
                    PaidHourInThisWeek = worksForWorker.Sum(x => x.PaidHourInThisWeek),
                    TotalHourInThisWeek = worksForWorker.Sum(x => x.TotalHourInThisWeek),
                    TotalHourInLastMonth = worksForWorker.Sum(x => x.TotalHourInLastMonth),

                });
            }
            return workersVm;
        }
    }
}
