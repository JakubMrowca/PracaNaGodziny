using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;
using Works.Shared.ValueObjects;

namespace Works.Shared.Commands
{
    public class CreateEmployer:ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public EmplyerInfo Data { get; set; }
    }
}
