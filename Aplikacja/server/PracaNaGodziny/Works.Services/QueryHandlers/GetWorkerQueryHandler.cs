using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Works.Models.Domain;
using Works.Models.Domain.Ref;
using Works.Models.Storage;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.QueryHandlers
{
    public class GetWorkerQueryHandler : IQueryHandler<GetWorker, WorkerVm>
    {
        private readonly IQueryable<Employer> _employers;
        private readonly IQueryable<Worker> _workers;
        private readonly IQueryable<LocationRef> _locations;
        private readonly IQueryBus _queryBus;

        public GetWorkerQueryHandler(WorkDbContext workDbContext, IQueryBus queryBus)
        {

            _queryBus = queryBus;
            _workers = workDbContext.Workers;
            _locations = workDbContext.Locations;
            _employers = workDbContext.Employers;
        }

        public async Task<WorkerVm> Handle(GetWorker message, CancellationToken cancellationToken)
        {
            var worker = await _workers
                .Where(x => x.Arch == false && x.Id == message.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var employer = await _employers
                .Where(x => x.Arch == false && x.Id == worker.EmployerId)
                .FirstOrDefaultAsync(cancellationToken);

            var worksForWorker = await
                _queryBus.Send<GetWorksForWorker, List<WorkSummaryVm>>(new GetWorksForWorker(worker.Id,message.From,message.To));

            return new WorkerVm
            {
                Address = worker.Address,
                Works = worksForWorker,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Employer = MapEmployerToVm(employer),
                Id = worker.Id,
                PaidHour = worksForWorker.Sum(x => x.PaidHour),
                TotalHour = worksForWorker.Sum(x => x.UnpaidHour),
                Wage = worksForWorker.Sum(x => x.Wage),
                TotalHourInThisMonth = worksForWorker.Sum(x => x.TotalHourInThisMonth),
                PaidHourInThisMonth = worksForWorker.Sum(x => x.PaidHourInThisMonth),
                PaidHourInThisWeek = worksForWorker.Sum(x => x.PaidHourInThisWeek),
                TotalHourInThisWeek = worksForWorker.Sum(x => x.TotalHourInThisWeek),
                TotalHourInLastMonth = worksForWorker.Sum(x => x.TotalHourInLastMonth),
                
            };

        }
        private EmployerVm MapEmployerToVm(Employer employer)
        {
            return new EmployerVm
            {
                AccountNumber = employer.AccountNumber,
                Id = employer.Id,   
                FirstName = employer.FirstName,
                LastName = employer.LastName,
                Address = employer.Address
            };
        }
    }
}
