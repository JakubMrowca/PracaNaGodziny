using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Users.Services.Services;
using Users.Shared.Commands;
using Works.Shared.Commands;
using Works.Shared.ValueObjects;

namespace WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly ILoggedUserService _loggedUserService;

        public UsersController(ICommandBus commandBus, IQueryBus queryBus, ILoggedUserService loggedUserService)
        {
            _loggedUserService = loggedUserService;
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
        public async Task<UserVm> AuthorizeUser([FromBody] AuthorizeUser command)
        {
           await _commandBus.Send(command);
           var loggedUser = _loggedUserService.GetLoggedUser();
           return loggedUser;
        }

        [HttpPost]
        [Route("photo")]
        public async Task AddPhoto([FromBody] AddPhotoForEmployer command)
        {
            await _commandBus.Send(command);
        }

    }
}
