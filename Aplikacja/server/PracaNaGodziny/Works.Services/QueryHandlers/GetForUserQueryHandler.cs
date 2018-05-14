using Infrastructure.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Works.Models.Domain;
using Works.Models.Storage;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace Works.Services.QueryHandlers
{
    public class GetForUserQueryHandler:IQueryHandler<GetForUser, UserVm>
    {
        private readonly IQueryable<Employer> _employers;
        private readonly IQueryable<Worker> _workers;

        public GetForUserQueryHandler(WorkDbContext workDbContext)
        {
            _workers = workDbContext.Workers;
            _employers = workDbContext.Employers;
        }

        public async Task<UserVm> Handle(GetForUser request, CancellationToken cancellationToken)
        {
            var employer = await _employers.Where(x => x.UserId == request.UserId && x.Arch == false).FirstOrDefaultAsync();
            var worker = await _workers.Where(x => x.UserId == request.UserId && x.Arch == false).FirstOrDefaultAsync();

            var userVm = new UserVm
            {
                Id = request.UserId,
                Photo = employer.Photo,
                EmployerId = employer?.Id,
                EmployerName = employer?.FirstName + " " + employer?.LastName,
                EmployerAddress = employer?.Address,
                WorkerId = worker?.Id,
                WorkerName = worker?.FirstName + " " + worker?.LastName,
                WorkerAddress = worker?.Address
            };
            return userVm;
        }
    }
}
