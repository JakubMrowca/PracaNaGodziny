using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;

namespace Users.Shared.Commands
{
    public class AuthorizeUser:ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
