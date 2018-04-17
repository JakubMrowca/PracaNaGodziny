using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Aggregates;
using Works.Shared.ValueObjects;

namespace Works.Models.Domain
{
    public class Worker : BaseAggregate
    {
        public Guid? UserId { get; set; }
        public Guid EmployerId { get; set; }

        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Address { get;  set; }
        public string AccountNumber { get; set; }

        public Worker()
        {

        }

        public Worker(Guid id, Guid? userId, Guid employerId, string firstName, string lastName, string address, string accountNumber)
        {
            Id = id;
            UserId = userId;
            EmployerId = employerId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountNumber = accountNumber;
        }

        public Worker(Guid id, Guid employerId, string firstName, string lastName, string address, string accountNumber)
            : this(id, null, employerId, firstName, lastName, address, accountNumber)
        {
        }

        public void Update(WorkerInfo data)
        {
            FirstName = data.FirstName;
            LastName = data.LastName;
            Address = data.Address;
        }
    }
}
