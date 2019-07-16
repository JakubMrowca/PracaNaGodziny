using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Domain.Aggregates
{
    public class BaseAggregate:IAggregate
    {
        public Guid Id { get; set; }
        public bool Arch { get; set; }
        public DateTime ModDateTime { get; set; }
    }
}
