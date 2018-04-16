using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Users.Shared.ValueObjects;

namespace Users.Shared.Commands
{
    public class UpdateUsers: ICommand
    {
        public Guid Id { get; set; }
        public UserInfo Data { get; set; }

        public UpdateUsers(Guid id, UserInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
