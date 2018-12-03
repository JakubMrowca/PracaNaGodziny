using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Notifications.Shared.Hubs;
using Users.Services.Services;
using Users.Shared.Commands;
using Works.Shared.Commands;
using Works.Shared.ValueObjects;

namespace WebApi.Controllers
{
    public interface IUsersController
    {
        Task CreateUser(CreateUser command);
        Task DeleteUser(string id);
        Task AuthorizeUser(AuthorizeUser command);
        Task AddPhoto(AddPhotoForEmployer command);
    }

    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class UsersController : Controller, IUsersController
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly ILoggedUsersMock _loggedUserService;
        private readonly IHubContext<UserHub> _hub;

        public UsersController(ICommandBus commandBus, IQueryBus queryBus, ILoggedUsersMock loggedUserService, IHubContext<UserHub> hub)
        {
            _loggedUserService = loggedUserService;
            _commandBus = commandBus;
            _queryBus = queryBus;
            _hub = hub;
        }

        [HttpPost]
        [Route("")]
        public async Task CreateUser([FromBody] CreateUser command)
        {
            await _commandBus.Send(command);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task DeleteUser(string id)
        {
            await _commandBus.Send(new DeleteUser(Guid.Parse(id)));
        }

        [HttpPost]
        [Route("authorize")]
        public async Task AuthorizeUser([FromBody] AuthorizeUser command)
        {
            await _commandBus.Send(command);
        }

        [HttpPost]
        [Route("photo")]
        public async Task AddPhoto([FromBody] AddPhotoForEmployer command)
        {
            await _commandBus.Send(command);
        }

    }
}
