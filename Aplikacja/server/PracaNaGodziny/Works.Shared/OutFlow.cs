using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared
{
    public class OutFlow
    {
        public DateTime TimeStamp { get; }
        public double Hours { get; }

        public OutFlow(DateTime timeStamp, double hours)
        {
            TimeStamp = timeStamp;
            Hours = hours;
        }
    }
}
