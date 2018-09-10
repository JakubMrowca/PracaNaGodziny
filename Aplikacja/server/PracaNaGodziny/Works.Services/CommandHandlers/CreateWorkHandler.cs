using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Marten;
using Marten.Events;
using MediatR;
using Works.Models.Domain;
using Works.Shared.Commands;


namespace Works.Services.CommandHandlers
{
    public class CreateWorkHandler : ICommandHandler<CreateNewWork>
    {
        private readonly IDocumentSession _session;

        private IEventStore _store => _session.Events;

        public CreateWorkHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Unit> Handle(CreateNewWork command, CancellationToken cancellationToken = default(CancellationToken))
        {
            //if (!_session.Query<ClientsView>().Any(c => c.Id == command.ClientId))
            //    throw new ArgumentException("Client does not exist!", nameof(command.ClientId));

            var work = new Work(command.Id, command.WorkerId, command.LocationId, command.Rate);

            _store.Append(work.Id, work.PendingEvents.ToArray());
            await _session.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
