using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Events;
using Marten;
using Marten.Events;
using Works.Shared.Events;

namespace Clients.Services.EventHandlers
{
    public class ClientCreatedEventHandler : IEventHandler<ClientCreated>
    {
        private readonly IDocumentSession _session;
        private IEventStore store => _session.Events;

        public ClientCreatedEventHandler(IDocumentSession session)
        {
            _session = session;
        }

        public Task Handle(ClientCreated @event, CancellationToken cancellationToken)
        {
            store.Append(@event.Id, @event);
            return _session.SaveChangesAsync(cancellationToken);
        }
    }
}
