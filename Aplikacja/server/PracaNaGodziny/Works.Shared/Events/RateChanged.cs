using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;

namespace Works.Shared.Events
{
    public class RateChanged : IEvent
    {
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }

        public Guid RateId
        {
            get;
            set;
        }

        public Guid WorkId { get; set; } 
    }
}
