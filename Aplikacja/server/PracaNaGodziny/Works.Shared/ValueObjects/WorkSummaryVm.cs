using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class WorkSummaryVm
    {
        public Guid WorkId { get; set; }
        public Guid WorkerId { get; set; }
        public Guid LocationId { get; set; }
        public decimal Wage { get;  set; }
        public double UnpaidHour { get;  set; }
        public double PaidHour { get;  set; }
        public double AdditionalHour { get;  set; }
        public decimal AdditionalWage { get;  set; }
        public double Rate { get; set; }
    }
}
