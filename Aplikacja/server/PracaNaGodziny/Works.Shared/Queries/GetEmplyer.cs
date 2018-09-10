using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Queries;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetEmplyer:IQuery<EmployerVm>
    {
        public Guid Id { get; }

        public GetEmplyer(Guid id)
        {
            Id = id;
        }
    }
}
