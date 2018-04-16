using System;
using System.Collections.Generic;
using System.Text;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Events;

namespace Works.Shared.Events
{
    public class ClientCreated : BaseEvent
    {
        public Guid Id { get;}
        public Guid EmployerId { get; }
        public ClientInfo Data { get;}

        public ClientCreated(Guid id, Guid employerId, ClientInfo data)
        {
            Id = id;
            EmployerId = employerId;
            Data = data;
        }
    }
}
