using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Aggregates;

namespace Works.Models.Domain.Ref
{
    public class LocationRef : BaseAggregate
    {
        public Guid? ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public Guid? EmployerId { get; set; }

        public LocationRef()
        {

        }

        public LocationRef(Guid id, string name, string address, Guid? clientId = null, Guid? employerId = null)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
            Address = address;
            EmployerId = employerId;
        }
    }
}
