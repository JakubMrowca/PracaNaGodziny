using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Aggregates;

namespace Works.Models.Domain.Ref
{
    public class LocationRef : BaseAggregate
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public LocationRef()
        {
        }

        public LocationRef(Guid id, Guid clientId, string name, string address)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
            Address = address;
        }
    }
}
