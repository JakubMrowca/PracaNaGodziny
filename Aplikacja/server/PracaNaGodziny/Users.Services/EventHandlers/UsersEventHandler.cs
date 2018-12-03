using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Events;
using Marten;
using Marten.Events;
using Users.Shared.Events;

namespace Users.Services.EventHandlers
{
    public class UsersEventHandler :
        IEventHandler<UserCreated>,
        IEventHandler<UserUpdated>,
        IEventHandler<UserDeleted>
    {

        private readonly IDocumentSession _session;
        private IEventStore store => _session.Events;

        public UsersEventHandler(IDocumentSession session)
        {
            _session = session;
        }

        public Task Handle(UserDeleted @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            store.Append(@event.Id, @event);
            return _session.SaveChangesAsync();
        }

        public Task Handle(UserUpdated @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            store.Append(@event.Id, @event);
            return _session.SaveChangesAsync();
        }

        public Task Handle(UserCreated @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            store.Append(@event.Id, @event);
            return _session.SaveChangesAsync();
        }

    }
}
