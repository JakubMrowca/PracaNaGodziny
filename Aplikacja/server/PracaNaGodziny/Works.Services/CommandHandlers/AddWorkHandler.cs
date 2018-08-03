using Clients.Shared.Commands;
using Clients.Shared.Queries;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Marten;
using Marten.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Models.View;
using Works.Shared.Commands;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.CommandHandlers
{
    public class AddWorkHandler : ICommandHandler<AddWorkCommand>
    {
        private readonly IDocumentSession _session;

        private IEventStore _store => _session.Events;
        private IQueryBus _queryBus;
        private ICommandBus _commandBus;
        private WorkDbContext _workDbContext;
        public AddWorkHandler(IDocumentSession session, IQueryBus queryBus, WorkDbContext context, ICommandBus commandBus)
        {
            _commandBus = commandBus;
            _session = session;
            _queryBus = queryBus;
            _workDbContext = context;
        }

        public async Task<Unit> Handle(AddWorkCommand command, CancellationToken cancellationToken)
        {
            var location = await _queryBus.Send<GetLocation, Clients.Shared.ValueObjects.LocationVm>(new GetLocation(command.LocationId.HasValue ? command.LocationId.Value : Guid.Empty));

            if (location == null)
            {
                var locationId = Guid.NewGuid();
                var createLocationCommand = new CreateLocation
                {
                    EmployerId = command.EmployerId,
                    Data = new LocationInfo { Name = command.LocationName },
                    Id = locationId
                };
                await _commandBus.Send(createLocationCommand);
                location = await _queryBus.Send<GetLocation, Clients.Shared.ValueObjects.LocationVm>(new GetLocation(locationId));
            }
            WorkSummaryVm work = null;
            try
            {
                work = await _queryBus.Send<GetWorkByLocationAndWorker, WorkSummaryVm>(new GetWorkByLocationAndWorker { WorkerId = command.WorkerId, LocationId = location.Id });
            }
            catch (Exception ex)
            {
                work = null;
            }

            if(work == null)
            {
                var workId = Guid.NewGuid();
                var createWorkCommand = new CreateNewWork() { Id = workId, LocationId = location.Id, WorkerId = command.WorkerId, Rate = command.Rate };
                await _commandBus.Send(createWorkCommand);
                work = await _queryBus.Send<GetWork, WorkSummaryVm>(new GetWork(workId));
            }

            var addHourCommand = new AddHours()
            {
                AddidtionalRate = command.AdditionalHours,
                Hours = command.Hours,
                TimeStamp = command.WorkDate,
                WorkId = work.WorkId
            };

            await _commandBus.Send(addHourCommand);
            return Unit.Value;

        }
    }
}
