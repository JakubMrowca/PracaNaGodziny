using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;
using Works.Shared.ValueObjects;

namespace Works.Shared.Events
{
    public class WorkerCreated: BaseEvent
    {
        public Guid Id { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid EmployerId { get; private set; }

        public WorkerInfo Data { get; private set; }

        public WorkerCreated(Guid id, Guid? userId, Guid employerId, WorkerInfo data)
        {
            Id = id;
            UserId = userId;
            EmployerId = employerId;
            Data = data;
        }
    }
}
