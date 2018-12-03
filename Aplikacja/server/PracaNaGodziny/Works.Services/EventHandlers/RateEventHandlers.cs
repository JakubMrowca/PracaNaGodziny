using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Events;
using Marten;
using Marten.Events;
using MediatR;
using Works.Models.Domain;
using Works.Shared.Events;

namespace Works.Services.EventHandlers
{
    public class RateEventHandlers : IEventHandler<RateChanged>
    {
        private readonly IDocumentSession _session;
        private IEventStore _store => _session.Events;

        public RateEventHandlers(IDocumentSession session)
        {
            _session = session;
        }

        public async Task Handle(RateChanged command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var rate = await _store.AggregateStreamAsync<Work>(command.WorkId, token: cancellationToken);

            rate.RateChange(command.TimeStamp, command.Value);
            _store.Append(rate.Id, rate.PendingEvents.ToArray());

            await _session.SaveChangesAsync(cancellationToken);
        }
    }
}
