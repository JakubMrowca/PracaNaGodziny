using System;

namespace Infrastructure.Domain.Aggregates
{
    public interface IAggregate
    {
        Guid Id { get; }
    }
}
