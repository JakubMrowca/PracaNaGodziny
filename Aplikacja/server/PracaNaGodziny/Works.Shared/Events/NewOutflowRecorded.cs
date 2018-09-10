using Infrastructure.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Events
{
    public class NewOutflowRecorded :IEvent
    {
        public Guid ForWorkId { get; }
        public OutFlow OutFlow { get; }

        public NewOutflowRecorded(Guid forWorkId, OutFlow outFlow)
        {
            ForWorkId = forWorkId;
            OutFlow = outFlow;
        }
    }
}
