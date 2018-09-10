using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Commands;
using Users.Shared.ValueObjects;

namespace Users.Shared.Commands
{
    public class CreateUser : ICommand
    {
        public Guid Id { get; set; }
        //public UserInfo Data { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsEmployer { get; set; }
        public bool IsWorker { get; set; }
        public CreateUser()
        {
            Id = Guid.NewGuid();
        }
    }

}
