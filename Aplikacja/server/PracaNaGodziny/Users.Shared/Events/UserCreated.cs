using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;
using Users.Shared.ValueObjects;

namespace Users.Shared.Events
{
    public class UserCreated: BaseEvent
    {
        public Guid Id { get; }
        public UserInfo Data { get; }

        public UserCreated(Guid id, UserInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
