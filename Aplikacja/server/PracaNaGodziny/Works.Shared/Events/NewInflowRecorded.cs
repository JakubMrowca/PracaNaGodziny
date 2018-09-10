using Infrastructure.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Events
{
    public class NewInflowRecorded:IEvent
    {
        public Guid ForWorkId { get; }
        public InFlow Inflow { get; }

        public NewInflowRecorded(Guid forWorkId, InFlow inflow)
        {
            ForWorkId = forWorkId;
            Inflow = inflow;
        }
    }
}
