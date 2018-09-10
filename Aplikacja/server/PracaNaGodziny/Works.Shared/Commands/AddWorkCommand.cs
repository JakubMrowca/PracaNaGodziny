using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Commands
{
    public class AddWorkCommand:ICommand
    {
        public Guid? LocationId { get; set; }
        public Guid WorkerId { get; set; }
        public string LocationName { get; set; }
        public int Rate { get; set; }
        public Guid EmployerId { get; set; }
        public DateTime WorkDate { get; set; }
        public double Hours { get; set; }
        public double AdditionalHours { get; set; }
        public double AdditionalRate { get; set; }
    }
}
