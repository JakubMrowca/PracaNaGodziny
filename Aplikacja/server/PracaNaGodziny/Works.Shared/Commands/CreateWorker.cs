using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;
using Works.Shared.ValueObjects;

namespace Works.Shared.Commands
{
    public class CreateWorker: ICommand
    {
        public Guid Id { get; set; }
        public Guid EmployerId { get; set; }
        public Guid? UserId { get; set; }
        public WorkerInfo Data { get; set; }

    }
}
