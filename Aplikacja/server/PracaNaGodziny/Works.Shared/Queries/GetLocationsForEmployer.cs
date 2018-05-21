using Infrastructure.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetLocationsForEmployer:IQuery<List<LocationVm>>
    {
        public Guid EmployerId { get; set; }
    }
}
