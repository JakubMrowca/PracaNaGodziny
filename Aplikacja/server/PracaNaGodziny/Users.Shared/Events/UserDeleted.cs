using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;

namespace Users.Shared.Events
{
    public class UserDeleted: BaseEvent
    {
        public Guid Id { get; }

        public UserDeleted(Guid id)
        {
            Id = id;
        }
    }
}
