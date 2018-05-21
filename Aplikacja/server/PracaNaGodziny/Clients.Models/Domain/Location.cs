using Infrastructure.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clients.Models.Domain
{
    public class Location : BaseAggregate
    {
        public Guid? ClientId { get; set; }
        public Client Client { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public Guid? EmployerId { get; set; }

        public Location()
        {

        }

        public Location(Guid id, string name, string address, Guid? clientId = null, Guid? employerId = null)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
            Address = address;
            EmployerId = employerId;
        }
    }
}
