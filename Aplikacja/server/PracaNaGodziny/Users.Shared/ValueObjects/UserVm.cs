using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Shared.ValueObjects
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public bool HasPassword { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
