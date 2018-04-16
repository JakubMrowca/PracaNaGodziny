using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Commands
{
    public class AddHours :ICommand
    {
        public Guid WorkId { get; set; }
        public double Hours { get; set; }
        public double? AddidtionalRate { get; set; }
    }
}
