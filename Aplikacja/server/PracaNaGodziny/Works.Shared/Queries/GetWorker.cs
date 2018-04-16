using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Queries;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetWorker:IQuery<WorkerVm>
    {
        public Guid Id { get; }

        public GetWorker(Guid id)
        {
            Id = id;
        }
    }
}
