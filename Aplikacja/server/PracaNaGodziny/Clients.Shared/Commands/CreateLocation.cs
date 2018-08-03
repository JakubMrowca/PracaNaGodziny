using System;
using System.Collections.Generic;
using System.Text;
using Clients.Shared.ValueObjects;
using Infrastructure.Domain.Commands;

namespace Clients.Shared.Commands
{
    public class CreateLocation : ICommand
    {
        public Guid Id { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? EmployerId { get; set; }
        public LocationInfo Data { get; set; }
        
    }
}
