using Infrastructure.Domain.Commands;
using Marten;
using Marten.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Works.Models.Domain;
using Works.Shared.Commands;

namespace Works.Services.CommandHandlers
{
    public class HoursHandler:ICommandHandler<AddHours>, ICommandHandler<SubstractHours>
    {
        private readonly IDocumentSession _session;
        private IEventStore _store => _session.Events;

        public HoursHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task Handle(AddHours command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var workFor = await _store.AggregateStreamAsync<Work>(command.WorkId, token: cancellationToken);

            workFor.RecordInflow(command.TimeStamp, command.Hours,command.AddidtionalRate);
            _store.Append(workFor.Id, workFor.PendingEvents.ToArray());

            await _session.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(SubstractHours command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var workFor = await _store.AggregateStreamAsync<Work>(command.WorkId, token: cancellationToken);

            workFor.RecordOutflow(command.TimeStamp, command.Hours);
            _store.Append(workFor.Id, workFor.PendingEvents.ToArray());

            await _session.SaveChangesAsync(cancellationToken);
        }
    }
}
