using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Queries;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetWorker : IQuery<WorkerVm>
    {
        public Guid Id { get; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public GetWorker(Guid id, DateTime? from = null, DateTime? to = null)
        {
            Id = id;
            From = from;
            To = to;
        }
    }
}
