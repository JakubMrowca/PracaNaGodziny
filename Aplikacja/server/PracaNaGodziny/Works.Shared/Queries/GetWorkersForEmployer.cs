using Infrastructure.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Works.Shared.ValueObjects;

namespace Works.Shared.Queries
{
    public class GetWorkersForEmployer : IQuery<List<WorkerVm>>
    {
        public string EmployerId { get; set; }
    }
}
