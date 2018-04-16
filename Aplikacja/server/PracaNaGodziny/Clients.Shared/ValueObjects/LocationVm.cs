using System;

namespace Works.Shared.Queries
{
    public class LocationVm
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ClientName { get; set; }
    }
}