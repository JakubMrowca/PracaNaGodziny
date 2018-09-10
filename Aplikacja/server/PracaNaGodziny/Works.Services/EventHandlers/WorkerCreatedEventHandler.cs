using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Events;
using Marten;
using Marten.Events;
using Works.Shared.Events;

namespace Works.Services.EventHandlers
{
    public class WorkerCreatedEventHandler : IEventHandler<WorkerCreated>
    {
        private readonly IDocumentSession _session;
        private IEventStore store => _session.Events;

        public WorkerCreatedEventHandler(IDocumentSession session)
        {
            _session = session; 
        }

        public Task Handle(WorkerCreated @event, CancellationToken cancellationToken)
        {
            store.Append(@event.Id, @event);
            return _session.SaveChangesAsync();
        }
    }
}
