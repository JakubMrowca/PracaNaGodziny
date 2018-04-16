using Infrastructure.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using Users.Shared.ValueObjects;

namespace Users.Models.Domain
{
    public class User : BaseAggregate
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User()
        {

        }

        public User(Guid id, string login, string password, string email)
        {
            Id = id;
            Login = login;
            Password = password;
            Email = email;
        }

        public void Update(UserInfo info)
        {
            Login = info.Login;
            Email = info.Email;
            Password = info.Password;
            ModDateTime = DateTime.Now;
        }

        public void Delete()
        {
            Arch = true;
            ModDateTime = DateTime.Now;
        }
    }
}
