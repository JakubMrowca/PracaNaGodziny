using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class WorkerInfo
    {
        public Guid EmployerId { get; set; }
        public Guid? UserId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
    }
}
