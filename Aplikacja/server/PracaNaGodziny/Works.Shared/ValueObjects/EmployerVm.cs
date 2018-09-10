//using Clients.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class EmployerVm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public byte[] Photo { get; set; }

        public List<WorkerVm> Workers { get; set; }
        public List<ClientVm> Clients { get; set; }
    }
}
