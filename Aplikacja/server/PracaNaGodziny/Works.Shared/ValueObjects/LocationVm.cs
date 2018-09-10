using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.ValueObjects
{
    public class LocationVm
    {
        public Guid Id { get; set; }
        public Guid? ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ClientName { get; set; }
        public ClientVm Client { get; set; }
        public double PaidHour { get; set; }
        public double TotalHour { get; set; }
        public double UnpaidHour => TotalHour - PaidHour;
        public decimal Wage { get; set; }

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
