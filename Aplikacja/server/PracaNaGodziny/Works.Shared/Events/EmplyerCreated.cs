using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;
using Works.Shared.ValueObjects;

namespace Works.Shared.Events
{
    public class EmplyerCreated:BaseEvent
    {
        public Guid Id { get;}
        public Guid UserId { get; }
        public EmplyerInfo Data { get;}

        public EmplyerCreated(Guid id, Guid userId, EmplyerInfo data)
        {
            Id = id;
            UserId = userId;
            Data = data;
        }
    }
}
