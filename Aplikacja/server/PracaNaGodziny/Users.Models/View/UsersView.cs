using System;
using System.Collections.Generic;
using System.Text;
using Marten.Events.Projections;
using Users.Shared.Events;

namespace Users.Models.View
{
    public class UsersView
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        internal void ApplyEvent(UserCreated @event)
        {
            Id = @event.Id;
            Email = @event.Data.Email;
            Login = @event.Data.Login;
            IsDeleted = false;
        }

        internal void ApplyEvent(UserUpdated @event)
        {
            Email = @event.Data.Email;
            Login = @event.Data.Login;
        }

        internal void ApplyEvent(UserDeleted @event)
        {
            IsDeleted = true;
        }
    }
}
