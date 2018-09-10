using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class WorkerController: Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public WorkerController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<WorkerVm> GetWorker(string id)
        {
            var query = new GetWorker(Guid.Parse(id));
            return await _queryBus.Send<GetWorker, WorkerVm>(query);
        }

    }
}
