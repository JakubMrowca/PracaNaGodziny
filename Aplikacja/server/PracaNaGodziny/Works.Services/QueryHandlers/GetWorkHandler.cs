using Infrastructure.Domain.Queries;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Works.Models.View;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;


namespace Works.Services.QueryHandlers
{
    public class GetWorkHandler : IQueryHandler<GetWork, WorkSummaryVm>
    {
        private readonly IDocumentSession _session;

        public GetWorkHandler(IDocumentSession session)
        {
            _session = session;
        }

        public Task<WorkSummaryVm> Handle(GetWork message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session
                .Query<WorkSummaryView>()
                .Select(a => new WorkSummaryVm
                {
                    WorkId = a.WorkId,
                    Wage = a.Wage,
                    LocationId = a.LocationId,
                    WorkerId = a.WorkerId,
                    AdditionalHour = a.AdditionalHour,
                    PaidHour = a.PaidHour,
                    UnpaidHour = a.UnpaidHour,
                    AdditionalWage = a.AdditionalWage,
                    Rate = a.Rate
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
