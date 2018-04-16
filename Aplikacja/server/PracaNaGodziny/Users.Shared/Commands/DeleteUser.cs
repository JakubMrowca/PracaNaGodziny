using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;

namespace Users.Shared.Commands
{
    public class DeleteUser: ICommand
    {
        public Guid Id { get; }

        public DeleteUser(Guid id)
        {
            Id = id;
        }
    }

}
