using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public Guid? EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public byte[] Photo { get; set; }
        public Guid? WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string WorkerAddress { get; set; }
    }
}
