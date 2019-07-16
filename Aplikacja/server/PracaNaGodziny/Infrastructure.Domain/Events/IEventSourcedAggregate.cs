using System.Collections.Generic;
using Infrastructure.Domain.Aggregates;

namespace Infrastructure.Domain.Events
{
    public interface IEventSourcedAggregate: IAggregate
    {
        Queue<IEvent> PendingEvents { get; }
    }
}
