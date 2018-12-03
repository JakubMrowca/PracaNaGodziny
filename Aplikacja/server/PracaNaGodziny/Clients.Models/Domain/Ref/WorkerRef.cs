using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Aggregates;

namespace Clients.Models.Domain.Ref
{
    public class WorkerRef:BaseAggregate
    {
        public Guid? UserId { get; set; }
        public Guid EmployerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public byte[] Photo { get; set; }

        public WorkerRef()
        {

        }

        public WorkerRef(Guid id, Guid? userId, Guid employerId, string firstName, string lastName, string address, string accountNumber)
        {
            Id = id;
            UserId = userId;
            EmployerId = employerId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountNumber = accountNumber;
        }

        public WorkerRef(Guid id, Guid employerId, string firstName, string lastName, string address, string accountNumber)
            : this(id, null, employerId, firstName, lastName, address, accountNumber)
        {
        }
    }
}
