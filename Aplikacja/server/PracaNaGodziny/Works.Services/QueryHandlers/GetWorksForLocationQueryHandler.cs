using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Queries;
using Marten;
using Marten.Events;
using Works.Models.View;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.QueryHandlers
{
    public class GetWorksForLocationQueryHandler: IQueryHandler<GetWorksForLocation, List<WorkSummaryVm>>
    {
        private readonly IDocumentSession _session;
        private IEventStore _store => _session.Events;
        private readonly IQueryBus _queryBus;

        public GetWorksForLocationQueryHandler(IDocumentSession session, IQueryBus queryBus)
        {
            _queryBus = queryBus;
            _session = session;
        }

        public async Task<List<WorkSummaryVm>> Handle(GetWorksForLocation message,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var worksForLocation = await _session.Query<WorkSummaryView>()
                .Where(x => x.LocationId == message.LocationId)
                .ToListAsync(cancellationToken);

            var worksVm = new List<WorkSummaryVm>();

            foreach (var work in worksForLocation)
            {
                worksVm.Add(await _queryBus.Send<GetWork, WorkSummaryVm>(new GetWork(work.Id, message.From, message.To)));
            }
            return await Task.FromResult(worksVm);

        }
    }
}
