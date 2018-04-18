using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;

namespace Works.Shared.ValueObjects
{
    public class WorkStat
    {
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

        public void Add(DateTime timeStamp, decimal wage, double hour)
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            if (timeStamp >= first && timeStamp <= last)
            {
                TotalHourInLastMonth += hour;
            }
            if (timeStamp.Day >= 1 && timeStamp.Day < DateTime.Now.Day)
            {
                WageInThisMonth += wage;
                TotalHourInThisMonth += hour;
            }
            if (timeStamp.DayOfWeek >= DayOfWeek.Monday && timeStamp.DayOfWeek <= DateTime.Now.DayOfWeek)
            {
                WageInThisWeek += wage;
                TotalHourInThisWeek += hour;
            }

        }

        public void AddAditional(DateTime timeStamp, decimal wage, double hour)
        {
            if (timeStamp.Day >= 1 && timeStamp.Day < DateTime.Now.Day)
            {
                AdditionalWageInThisMonth += wage;
                AdditionalHourInThisMonth += hour;
            }
            if (timeStamp.DayOfWeek >= DayOfWeek.Monday && timeStamp.DayOfWeek <= DateTime.Now.DayOfWeek)
            {
                AdditionalWageInThisWeek += wage;
                AdditionalHourInThisWeek += hour;
            }
        }

        public void Substract(DateTime timeStamp, decimal wage, double hour)
        {
            if (timeStamp.Day >= 1 && timeStamp.Day < DateTime.Now.Day)
            {
                WageInThisMonth -= wage;
                PaidHourInThisMonth -= hour;
            }
            if (timeStamp.DayOfWeek >= DayOfWeek.Monday && timeStamp.DayOfWeek <= DateTime.Now.DayOfWeek)
            {
                WageInThisWeek -= wage;
                PaidHourInThisWeek -= hour;
            }
        }
    }
}
