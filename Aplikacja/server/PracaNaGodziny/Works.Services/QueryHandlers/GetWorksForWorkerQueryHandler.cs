using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Queries;
using Marten;
using Marten.Events;
using Works.Models.Domain;
using Works.Models.View;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.QueryHandlers
{
    public class GetWorksForWorkerQueryHandler : IQueryHandler<GetWorksForWorker, List<WorkSummaryVm>>
    {
        private readonly IDocumentSession _session;
        private IEventStore _store => _session.Events;
        private readonly IQueryBus _queryBus;

        public GetWorksForWorkerQueryHandler(IDocumentSession session, IQueryBus queryBus)
        {
            _queryBus = queryBus;
            _session = session;
        }

        public async Task<List<WorkSummaryVm>> Handle(GetWorksForWorker message,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            
            var worksForWorker = await _session.Query<WorkSummaryView>()
                .Where(x => x.WorkerId == message.WorkerId)
                .ToListAsync(cancellationToken);
            var worksVm = new List<WorkSummaryVm>();

            foreach (var work in worksForWorker)
            {
                worksVm.Add(await _queryBus.Send<GetWork,WorkSummaryVm>(new GetWork(work.Id,message.From,message.To)));

            }
            return await Task.FromResult(worksVm);
          
        }
    }
}