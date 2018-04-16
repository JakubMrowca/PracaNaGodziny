using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Queries;

namespace Works.Shared.Queries
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
