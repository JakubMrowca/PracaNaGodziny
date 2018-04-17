using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clients.Shared.Commands;
using Clients.Shared.Queries;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Commands;
using Users.Shared.Queries;
using Users.Shared.ValueObjects;
using Works.Models.Domain;
using Works.Shared.Commands;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;
using ClientVm = Clients.Shared.ValueObjects.ClientVm;

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
        [HttpGet("user/{userId}")]
        public async Task<UserVm> GetUser(Guid userId)
        {
            //var guid = Guid.NewGuid();
            //var command = new CreateUser(guid,
            //    new UserInfo
            //    {
            //        Email = "metinowy@gmail.com",
            //        Login = "JanPietrzak",
            //        Password = "123"
            //    });

            //await _commandBus.Send(command);
            return await _queryBus.Send<GetUser, UserVm>(new GetUser(userId));
        }

        [HttpGet("employer/{userId}")]
        public async Task<EmployerVm> GetEmployer(Guid userId)
        {
            //var guid = Guid.NewGuid();
            //var command = new CreateEmployer(guid,userId,
            //    new EmplyerInfo
            //    {
            //        AccountNumber = "98112322232332",
            //        FirstName = "Lech",
            //        LastName = "Czech",
            //        Address = "lukowica 51312"
            //    });

            //await _commandBus.Send(command);
            return await _queryBus.Send<GetEmplyer, EmployerVm>(new GetEmplyer(userId));
        }

        //[HttpGet("employer")]
        //public async Task<EmployerVm> GetEmployer()
        //{

        //}

        [HttpGet("worker/{employerId}")]
        public async Task<WorkerVm> GetWorker(Guid employerId)
        {
            //var guid = Guid.NewGuid();
            //var command = new CreateWorker(guid, employerId,
            //    new WorkerInfo()
            //    {
            //        AccountNumber = "123232332",
            //        FirstName = "Jan",
            //        LastName = "Seba",
            //        Address = "wojewoda 40",
            //        EmployerId = employerId
            //    });

            //await _commandBus.Send(command);
            return await _queryBus.Send<GetWorker, WorkerVm>(new GetWorker(employerId));
        }

        [HttpGet("client/{employerId}")]
        public async Task<ClientVm> GetClient(Guid employerId)
        {
            //var guid = Guid.NewGuid();
            //var command = new CreateClient(guid, employerId,
            //    new ClientInfo
            //    {
            //        FirstName = "Jan",
            //        LastName = "Seba",
            //        Address = "wojewoda 40",
            //        Email = "9789798@dsds.pl",
            //        Phone = "232323"
            //    });

            //await _commandBus.Send(command);
            return await _queryBus.Send<GetClient, ClientVm>(new GetClient(employerId));
        }

        [HttpGet("location/{clientId}")]
        public async Task<Clients.Shared.ValueObjects.LocationVm> GetLocation(Guid clientId)
        {
            var guid = Guid.NewGuid();
            var command = new CreateLocation(guid,
                new LocationInfo
                {
                   Address = "wojewoda 40",
                   Name = "Budowa ul40",
                    
                }, clientId);

            await _commandBus.Send(command);
            return await _queryBus.Send<GetLocation, Clients.Shared.ValueObjects.LocationVm>(new GetLocation(guid));
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
