using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Commands
{
    public class CreateNewWork : ICommand
    {
        public Guid WorkerId { get; set; }
        public Guid LocationId { get; set; }
        public double Rate { get; set; }
    }
}
