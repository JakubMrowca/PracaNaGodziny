using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class ClientVm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<LocationVm> Locations { get; set; }

        public string EmployerName { get; set; }
        public Guid EmployerId { get; set; }
    }
}
