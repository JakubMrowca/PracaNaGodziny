using System;

namespace Works.Shared
{
    public class InFlow
    {
        public DateTime TimeStamp { get; }
        public double Hours { get;}
        public double? AdditionalRate { get;}

        public InFlow(DateTime timeStamp, double hours, double? additionalRate)
        {
            TimeStamp = timeStamp;
            Hours = hours;
            AdditionalRate = additionalRate;
        }
    }
}
