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
        public GetWork(Guid workId)
        {
            WorkId = workId;
        }
    }
}
