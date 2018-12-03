using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;

namespace Notifications.Shared
{
    public class SendRequestCommand:ICommand
    {
        public Guid employerId;
        public Guid workerId;
        public Guid locationId;
    }
}
