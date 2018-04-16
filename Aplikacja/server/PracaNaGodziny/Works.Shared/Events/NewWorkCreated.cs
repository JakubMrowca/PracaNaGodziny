using Infrastructure.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Events
{
    public class NewWorkCreated :IEvent
    {
        public Guid WorkId { get; set; }

        public Guid LocationId { get; set; }

        public Guid WorkerId { get; set; }

        public double Rate { get; set; }

    }
}
