using System;
using System.Collections.Generic;
using System.Text;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Queries;

namespace Clients.Shared.Queries
{
    public class GetLocation:IQuery<LocationVm>
    {
        public Guid Id { get; }

        public GetLocation(Guid id)
        {
            Id = id;
        }
    }
}
