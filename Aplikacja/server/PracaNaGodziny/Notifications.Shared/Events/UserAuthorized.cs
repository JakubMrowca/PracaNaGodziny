using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;

namespace Notifications.Shared.Events
{
    public class UserAuthorized:IEvent
    {
        public Guid UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
