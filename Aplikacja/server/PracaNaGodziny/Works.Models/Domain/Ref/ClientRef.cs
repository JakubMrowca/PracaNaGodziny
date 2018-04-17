using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Aggregates;

namespace Works.Models.Domain.Ref
{
    public class ClientRef : BaseAggregate
    {
        public Guid EmployerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ClientRef()
        {
        }

        public ClientRef(Guid id, Guid employerId, string firstName, string lastName, string address, string email, string phone)
        {
            Id = id;
            EmployerId = employerId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            Phone = phone;
        }
    }
}
