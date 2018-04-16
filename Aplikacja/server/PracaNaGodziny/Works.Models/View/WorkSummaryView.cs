using System;
using System.Collections.Generic;
using System.Text;
using Works.Shared.Events;

namespace Works.Models.View
{
    public class WorkSummaryView
    {
        public Guid Id { get; set; }
        public Guid WorkerId { get;  set; }
        public Guid WorkId { get;  set; }
        public Guid LocationId { get;  set; }

        public double Rate { get;  set; }
        public decimal Wage { get;  set; }

        public double UnpaidHour { get;  set; }
        public double PaidHour { get;  set; }
        public double AdditionalHour { get;  set; }
        public decimal AdditionalWage { get;  set; }


        public void Apply(NewWorkCreated @event)
        {
            WorkId = @event.WorkId;
            LocationId = @event.LocationId;
            WorkerId = @event.WorkerId;
            Rate = @event.Rate;
        }

        public void Apply(NewInflowRecorded @event)
        {
            if (!@event.Inflow.AdditionalRate.HasValue)
            {
                Wage += Convert.ToDecimal(@event.Inflow.Hours * Rate);
                UnpaidHour += @event.Inflow.Hours;
            }
            else
            {
                AdditionalWage += Convert.ToDecimal(@event.Inflow.Hours * (Rate + @event.Inflow.AdditionalRate.Value));
                AdditionalHour += @event.Inflow.Hours;
            }
        }

        public void Apply(NewOutflowRecorded @event)
        {
            Wage -= Convert.ToDecimal(@event.OutFlow.Hours * Rate);
            PaidHour += @event.OutFlow.Hours;
        }
    }
}
