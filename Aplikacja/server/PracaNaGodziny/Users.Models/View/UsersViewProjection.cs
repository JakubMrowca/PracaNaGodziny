using System;
using System.Collections.Generic;
using System.Text;
using Marten.Events.Projections;
using Users.Shared.Events;

namespace Users.Models.View
{
    public class UsersViewProjection : ViewProjection<UsersView, Guid>
    {
        public UsersViewProjection()
        {
            ProjectEvent<UserCreated>(ev => ev.Id, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<UserUpdated>(ev => ev.Id, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<UserDeleted>(ev => ev.Id, (view, @event) => view.ApplyEvent(@event));
        }
    }
}
