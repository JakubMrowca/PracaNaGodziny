using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Works.Shared.Commands;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class WorkController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public WorkController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost]
        [Route("")]
        public async Task AddWorker([FromBody] AddWorkerForEmployer command)
        {
            await _commandBus.Send(command);
        }

        [HttpPost]
        [Route("work")]
        public async Task AddWork([FromBody] AddWorkCommand command)
        {
            await _commandBus.Send(command);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<List<WorkerVm>> GetWorkersForEmployer(string id)
        {
            var query = new GetWorkersForEmployer
            {
                EmployerId = id
            };
            return await _queryBus.Send<GetWorkersForEmployer, List<WorkerVm>>(query);
        }

        [HttpGet]
        [Route("locations/{id}")]
        public async Task<List<LocationVm>> GetLocationsForEmployer(string id)
        {
            var query = new GetLocationsForEmployer
            {
                EmployerId = Guid.Parse(id)
            };
            return await _queryBus.Send<GetLocationsForEmployer, List<LocationVm>>(query);
        }
    }
}
