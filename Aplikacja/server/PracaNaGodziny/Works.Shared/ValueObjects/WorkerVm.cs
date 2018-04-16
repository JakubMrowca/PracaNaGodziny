using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class WorkerVm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public EmployerVm Employer { get; set; }
        public List<WorkSummaryVm> Works { get; set; }
    }
}
