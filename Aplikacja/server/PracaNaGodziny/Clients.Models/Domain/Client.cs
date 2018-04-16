using Infrastructure.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using Works.Models.Domain;

namespace Clients.Models.Domain
{
    public class Client : BaseAggregate
    {
        public Guid EmployerId { get; }
        //public Emplyer Employer { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Email { get; }
        public string Phone { get; }

        public Client(Guid id, Guid employerId, string firstName, string lastName, string address, string email, string phone)
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
