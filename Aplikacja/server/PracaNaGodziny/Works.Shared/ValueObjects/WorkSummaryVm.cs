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

        public double TotalHourInThisMonth { get; set; }
        public double TotalHourInThisWeek { get; set; }
        public double PaidHourInThisMonth { get; set; }
        public double PaidHourInThisWeek { get; set; }
        //
        public double AdditionalHourInThisMonth { get; set; }
        public decimal AdditionalWageInThisMonth { get; set; }
        public double AdditionalHourInThisWeek { get; set; }
        public decimal AdditionalWageInThisWeek { get; set; }

        //
        public decimal WageInThisMonth { get; set; }
        public decimal WageInThisWeek { get; set; }

        //
        public double TotalHourInLastMonth { get; set; }

    }
}
