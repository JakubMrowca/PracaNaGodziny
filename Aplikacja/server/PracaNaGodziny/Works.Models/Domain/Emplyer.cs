using Infrastructure.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using Marten.Services;
using Works.Shared.ValueObjects;

namespace Works.Models.Domain
{
    public class Employer: BaseAggregate
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public int WorkerCount { get; set; }
        public int LocationCount { get; set; }

        public Employer()
        {

        }

        public Employer(Guid id, Guid userId, string firstName, string lastName, string address, string accountNumber)
        {
            Id = id;
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountNumber = accountNumber;
        }

        public void Update(EmplyerInfo data)
        {
            FirstName = data.FirstName;
            LastName = data.LastName;
            Address = data.Address;
            AccountNumber = data.AccountNumber;
        }
    }
}
