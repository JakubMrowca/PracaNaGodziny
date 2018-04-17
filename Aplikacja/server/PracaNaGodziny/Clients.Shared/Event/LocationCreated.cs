using System;
using System.Collections.Generic;
using System.Text;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Events;

namespace Clients.Shared.Events
{
    public class LocationCreated : BaseEvent
    {
        public Guid Id { get; private set; }
        public Guid? ClientId { get; private set; }

        public LocationInfo Data { get; private set; }

        public LocationCreated(Guid id, Guid? clientId, LocationInfo data)
        {
            Id = id;
            ClientId = clientId;
            Data = data;
        }
    }
}
