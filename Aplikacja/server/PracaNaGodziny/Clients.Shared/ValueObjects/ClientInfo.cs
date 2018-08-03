﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Clients.Shared.ValueObjects
{
    public class ClientInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}