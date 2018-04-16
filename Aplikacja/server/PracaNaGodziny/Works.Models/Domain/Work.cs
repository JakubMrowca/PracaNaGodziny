using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;
using Works.Shared;
using Works.Shared.Events;

namespace Works.Models.Domain
{
    public class Work : EventSource
    {
        public Guid WorkerId { get; private set; }
        public Guid LocationId { get; private set; }

        public double Rate { get; private set; }
        public decimal Wage { get; private set; }

        public double UnpaidHour { get; private set; }
        public double PaidHour { get; private set; }
        public double AdditionalHour { get; private set; }
        public decimal AdditionalWage { get; private set; }

        public Work()
        {
        }

        public Work(Guid workerId, Guid locationId, double rate)
        {
            var @event = new NewWorkCreated
            {
                WorkId = Guid.NewGuid(),
                WorkerId = workerId,
                LocationId = locationId,
                Rate = rate
            };

            Apply(@event);
            Append(@event);
        }

        public void RecordInflow(double hours, double? additionalRate)
        {
            var @event = new NewInflowRecorded(Id, new InFlow(DateTime.Now, hours, additionalRate));
            Apply(@event);
            Append(@event);
        }

        public void RecordOutflow(double hours)
        {
            var @event = new NewOutflowRecorded(Id, new OutFlow(DateTime.Now, hours));
            Apply(@event);
            Append(@event);
        }

        public void Apply(NewWorkCreated @event)
        {
            Id = @event.WorkId;
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
            UnpaidHour -= @event.OutFlow.Hours;
        }
    }
}
