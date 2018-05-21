using System;
using System.Collections.Generic;
using System.Text;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Commands;

namespace Clients.Shared.Commands
{
    public class CreateLocation : ICommand
    {
        public Guid Id { get; }
        public Guid? ClientId { get; }
        public Guid? EmployerId { get; }
        public LocationInfo Data { get; }

        public CreateLocation(Guid id, LocationInfo data, Guid? clientId = null, Guid? employerId = null)
        {
            Id = id;
            ClientId = clientId;
            Data = data;
            EmployerId = employerId;
        }
    }
}
