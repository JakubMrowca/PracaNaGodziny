using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared
{
    public class OutfFlowAdditionalWage
    {
        public DateTime TimeStamp { get; }
        public double Hours { get; }
        public decimal Wage { get; }

        public OutfFlowAdditionalWage(DateTime timeStamp, double hours, decimal wage)
        {
            TimeStamp = timeStamp;
            Hours = hours;
            Wage = wage;
        }
    }
}
