using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clients.Shared.Queries;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;
using LocationVm = Clients.Shared.ValueObjects.LocationVm;

namespace WebApi.Controllers
{

    public interface IClientController
    {
        Task<LocationVm> GetLocation(string id);
    }

    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class ClientController : Controller, IClientController
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public ClientController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet]
        [Route("location/{id}")]
        public async Task<LocationVm> GetLocation(string id)
        {
            var query = new GetLocation(Guid.Parse(id));
            return await _queryBus.Send<GetLocation, LocationVm>(query);
        }
    }
}
