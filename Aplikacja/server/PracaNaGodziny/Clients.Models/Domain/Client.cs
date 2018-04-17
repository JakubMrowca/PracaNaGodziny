using Infrastructure.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using Works.Models.Domain;

namespace Clients.Models.Domain
{
    public class Client : BaseAggregate
    {
        public Guid EmployerId { get; set; }
        public Employer Emplyer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Location> Locations => _locations;
        protected readonly ICollection<Location> _locations;

        public Client()
        {
            _locations = new List<Location>();
        }
        public Client(Guid id, Guid employerId, string firstName, string lastName, string address, string email, string phone)
        {
            Id = id;
            EmployerId = employerId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            Phone = phone;

            _locations = new List<Location>();
        }
    }
}
