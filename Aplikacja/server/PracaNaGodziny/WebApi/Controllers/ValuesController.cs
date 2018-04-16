using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Commands;
using Users.Shared.Queries;
using Users.Shared.ValueObjects;
using Works.Shared.Commands;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public ValuesController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var command = new CreateUser(Guid.NewGuid(),
                new UserInfo
                {
                    Email = "9789672@gmail.com",
                    Login = "Marcin",
                    Password = "123"
                });

            await _commandBus.Send(command);

            //var commandSub = new SubstractHours
            //{
            //    WorkId = Guid.Parse("ea9b0106-61e5-4af6-956d-6774b18c4328"),
            //    Hours = 15
            //};
            //await _commandBus.Send(commandSub);

            //command = new AddHours
            //{
            //    WorkId = Guid.Parse("ea9b0106-61e5-4af6-956d-6774b18c4328"),
            //    Hours = 10
            //};
            //await _commandBus.Send(command);

            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public async Task Post()
        {
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Task<List<UserVm>> Get(int id)
        {
            return _queryBus.Send<GetUsers, List<UserVm>>(new GetUsers());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
