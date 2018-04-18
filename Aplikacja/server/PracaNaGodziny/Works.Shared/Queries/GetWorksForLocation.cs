using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Queries;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetWorksForLocation : IQuery<List<WorkSummaryVm>>
    {
        public Guid LocationId { get; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public GetWorksForLocation(Guid locationId, DateTime? from = null, DateTime? to = null)
        {
            LocationId = locationId;
            From = from;
            To = to;
        }
    }
}
