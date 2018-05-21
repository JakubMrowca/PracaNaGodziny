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
    public class GetWorkQueryHandler : IQueryHandler<GetWork, WorkSummaryVm>, IQueryHandler<GetWorkByLocationAndWorker, WorkSummaryVm> 
    {
        private readonly IDocumentSession _session;

        public GetWorkQueryHandler(IDocumentSession session)
        {
            _session = session;
        }

        public Task<WorkSummaryVm> Handle(GetWork message, CancellationToken cancellationToken = default(CancellationToken))
        {
            var workVm = new WorkSummaryView();
            workVm.From = message.From;
            workVm.To = message.To;

            var events = _session.Events.FetchStream(message.WorkId);

            foreach (var @event in events)
            {
                workVm.Apply(@event);
            }

            return Task.FromResult(new WorkSummaryVm
            {
                WorkId = workVm.WorkId,
                Wage = workVm.Wage,
                LocationId = workVm.LocationId,
                WorkerId = workVm.WorkerId,
                AdditionalHour = workVm.AdditionalHour,
                PaidHour = workVm.PaidHour,
                UnpaidHour = workVm.TotalHour,
                AdditionalWage = workVm.AdditionalWage,
                Rate = workVm.Rate,
                TotalHourInThisMonth = workVm.WorkStat.TotalHourInThisMonth,
                TotalHourInThisWeek = workVm.WorkStat.TotalHourInThisWeek,
                TotalHourInLastMonth = workVm.WorkStat.TotalHourInLastMonth,
                PaidHourInThisWeek = workVm.WorkStat.PaidHourInThisWeek,
                PaidHourInThisMonth = workVm.WorkStat.PaidHourInThisMonth

            });

        }

        public Task<WorkSummaryVm> Handle(GetWorkByLocationAndWorker message, CancellationToken cancellationToken)
        {
 
            var workVm = _session.Query<WorkSummaryView>()
           .Where(x => x.LocationId == message.LocationId && x.WorkerId == message.WorkerId)
           .FirstOrDefault();

            if (workVm == null)
                return null;

            return Task.FromResult(new WorkSummaryVm
            {
                WorkId = workVm.WorkId,
                Wage = workVm.Wage,
                LocationId = workVm.LocationId,
                WorkerId = workVm.WorkerId,
                AdditionalHour = workVm.AdditionalHour,
                PaidHour = workVm.PaidHour,
                UnpaidHour = workVm.TotalHour,
                AdditionalWage = workVm.AdditionalWage,
                Rate = workVm.Rate,
                TotalHourInThisMonth = workVm.WorkStat.TotalHourInThisMonth,
                TotalHourInThisWeek = workVm.WorkStat.TotalHourInThisWeek,
                TotalHourInLastMonth = workVm.WorkStat.TotalHourInLastMonth,
                PaidHourInThisWeek = workVm.WorkStat.PaidHourInThisWeek,
                PaidHourInThisMonth = workVm.WorkStat.PaidHourInThisMonth
            });
        }
    }
}
