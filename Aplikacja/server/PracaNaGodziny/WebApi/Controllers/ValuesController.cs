using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clients.Shared.Commands;
using Clients.Shared.Queries;
using Infrastructure.Domain.Commands;
using Infrastructure.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Works.Shared.Commands;
using Works.Shared.Queries;
using Works.Shared.ValueObjects;
using ClientVm = Clients.Shared.ValueObjects.ClientVm;
using LocationVm = Clients.Shared.ValueObjects.LocationVm;

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

        [HttpGet("user/{userId}")]
        public async Task<WorkSummaryVm> GetUser(Guid userId)
        {

            return await _queryBus.Send<GetWork, WorkSummaryVm>(new GetWork(userId));
        }

        [HttpGet("employer/{userId}")]
        public async Task<EmployerVm> GetEmployer(Guid userId)
        {
            return await _queryBus.Send<GetEmplyer, EmployerVm>(new GetEmplyer(userId));
        }

        [HttpGet("worker/{employerId}")]
        public async Task<WorkerVm> GetWorker(Guid employerId)
        {
            return await _queryBus.Send<GetWorker, WorkerVm>(new GetWorker(employerId));
        }

        [HttpGet("client/{employerId}")]
        public async Task<ClientVm> GetClient(Guid employerId)
        {
            return await _queryBus.Send<GetClient, ClientVm>(new GetClient(employerId));
        }

        [HttpGet("location/{clientId}")]
        public async Task<LocationVm> GetLocation(Guid clientId)
        {
            var guid = Guid.NewGuid();
            var command = new CreateLocation
            {
                Id = guid,
                ClientId = clientId,
                Data =
                {
                    Address = "wojewoda 40",
                    Name = "Budowa ul40"
                }
            };

            await _commandBus.Send(command);
            return await _queryBus.Send<GetLocation, LocationVm>(new GetLocation(guid));
        }


       
    }
}