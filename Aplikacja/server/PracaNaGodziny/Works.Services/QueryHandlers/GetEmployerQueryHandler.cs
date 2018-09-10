using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Works.Models.Domain;
using Works.Models.Domain.Ref;
using Works.Models.Storage;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.QueryHandlers
{
    public class GetEmployerQueryHandler : IQueryHandler<GetEmplyer, EmployerVm>
    {
        private readonly IQueryable<Employer> _employers;
        private readonly IQueryable<Worker> _workers;
        private readonly IQueryable<ClientRef> _clients;

        public GetEmployerQueryHandler(WorkDbContext workDbContext)
        {
            _workers = workDbContext.Workers;
            _clients = workDbContext.Clients;
            _employers = workDbContext.Employers;
        }

        public async Task<EmployerVm> Handle(GetEmplyer message, CancellationToken cancellationToken)
        {
            var employer = await _employers
                .Where(x => x.Arch == false && x.Id == message.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var workersForEmployer = await _workers
                .Where(x => x.Arch == false && x.EmployerId == employer.Id)
                .ToListAsync(cancellationToken);

            var clientsForEmployer = await _clients
                .Where(x => x.Arch == false && x.EmployerId == employer.Id)
                .ToListAsync(cancellationToken);

            return new EmployerVm
            {
                Address = employer.Address,
                FirstName = employer.FirstName,
                Photo = employer.Photo,
                LastName = employer.LastName,
                AccountNumber = employer.AccountNumber,
                Clients = MapClientsToVm(clientsForEmployer),
                Workers = MapWorkersToVm(workersForEmployer),
                Id = employer.Id
            };

        }
        private List<WorkerVm> MapWorkersToVm(IReadOnlyList<Worker> workers)
        {
            return workers.Select(x => new WorkerVm
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address
            })
            .ToList();
        }

        private List<ClientVm> MapClientsToVm(IReadOnlyList<ClientRef> clients)
        {
            return clients.Select(x => new ClientVm
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                Email = x.Email,
                EmployerId = x.EmployerId,
                Phone = x.Phone
            })
            .ToList();
        }
    }
}
