using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class LocationVm
    {
        public Guid Id { get; set; }
        public Guid? ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ClientName { get; set; }
        public ClientVm Client { get; set; }
    }
}
