using Infrastructure.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Events
{
    public class NewOutflowAdditionalWageRecorded:IEvent
    {
        public Guid WorkId { get; }

    }
}
