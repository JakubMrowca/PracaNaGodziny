using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Queries;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetWorksForWorker:IQuery<List<WorkSummaryVm>>
    {
        public Guid WorkerId { get; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public GetWorksForWorker(Guid workerId, DateTime? from = null, DateTime? to = null)
        {
            WorkerId = workerId;
            From = from;
            To = to;
        }
    }
}
