using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Commands
{
    public class AddWorkerForEmployer:ICommand
    {
        public Guid EmployerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
    }
}
