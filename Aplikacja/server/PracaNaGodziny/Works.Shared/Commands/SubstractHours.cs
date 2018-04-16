using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Commands
{
    public class SubstractHours:ICommand
    {
        public Guid WorkId { get; set; }
        public double Hours { get; set; }
    }
}
