using Infrastructure.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetWorkByLocationAndWorker: IQuery<WorkSummaryVm>
    {
        public Guid WorkerId { get; set; }
        public Guid LocationId { get; set; }
    }
}
