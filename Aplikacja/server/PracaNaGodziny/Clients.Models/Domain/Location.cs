using Infrastructure.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clients.Models.Domain
{
    public class Location : BaseAggregate
    {
        public Guid ClientId { get; }
        public string Name { get; }
        public string Address { get; }

        public Location(Guid id, Guid clientId, string name, string address)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
            Address = address;
        }
    }
}
