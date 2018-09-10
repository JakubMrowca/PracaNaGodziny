using System;
using System.Collections.Generic;
using System.Text;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Queries;

namespace Works.Shared.Queries
{
    public class GetClient:IQuery<ClientVm>
    {
        public Guid Id { get; }

        public GetClient(Guid id)
        {
            Id = id;
        }
    }
}
