using Infrastructure.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetWork: IQuery<WorkSummaryVm>
    {
        public Guid WorkId { get; private set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public GetWork(Guid workId, DateTime? from = null, DateTime? to = null)
        {
            WorkId = workId;
            From = from;
            To = to;
        }
    }
}
