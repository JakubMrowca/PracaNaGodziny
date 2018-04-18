using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Commands;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public UsersController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost]
        [Route("")]
        public async Task CreateUser([FromBody] CreateUser command)
        {
            await _commandBus.Send(command);
        }

        [HttpPost]
        [Route("authorize")]
        public async Task<IActionResult> AuthorizeUser([FromBody] AuthorizeUser command)
        {
           await _commandBus.Send(command);
           return Ok();
        }

    }
}
