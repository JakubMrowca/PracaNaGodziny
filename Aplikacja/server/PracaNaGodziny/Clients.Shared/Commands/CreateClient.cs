using System;
using System.Collections.Generic;
using System.Text;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Commands;

namespace Clients.Shared.Commands
{
    public class CreateClient: ICommand
    {
        public Guid Id { get; }
        public Guid EmployerId { get; }
   
        public ClientInfo Data { get; }

        public CreateClient(Guid id, Guid employerId, ClientInfo data)
        {
            Id = id;
            EmployerId = employerId;
            Data = data;
        }
    }
}
