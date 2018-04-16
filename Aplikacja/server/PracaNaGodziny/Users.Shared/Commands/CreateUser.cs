using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;
using Users.Shared.ValueObjects;

namespace Users.Shared.Commands
{
    public class CreateUser : ICommand
    {
        public Guid Id { get; }
        public UserInfo Data { get; }

        public CreateUser(Guid id, UserInfo data)
        {
            Id = id;
            Data = data;
        }
    }

}
