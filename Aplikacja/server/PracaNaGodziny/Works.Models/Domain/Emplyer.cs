using Infrastructure.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using Marten.Services;
using Works.Shared.ValueObjects;

namespace Works.Models.Domain
{
    public class Emplyer: BaseAggregate
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string AccountNumber { get; private set; }
        public int WorkerCount { get; private set; }
        public int LocationCount { get; private set; }

        public Emplyer(Guid id, Guid userId, string firstName, string lastName, string address, string accountNumber)
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
