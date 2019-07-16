using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Domain.Events;
using Works.Shared;
using Works.Shared.Events;

namespace Works.Models.Domain
{
    public class Work : EventSourcedAggregate
    {
        public Guid WorkerId { get; private set; }
        public Guid LocationId { get; private set; }

        public double Rate { get; private set; }
        public decimal Wage { get; private set; }

        public double TotalHour { get; private set; }
        public double PaidHour { get; private set; }
        public double AdditionalHour { get; private set; }
        public decimal AdditionalWage { get; private set; }

        public Work()
        {
        }

        public Work(Guid id, Guid workerId, Guid locationId, double rate)
        {
            Id = id;

            var @event = new NewWorkCreated
            {
                WorkId = id,
                WorkerId = workerId,
                LocationId = locationId,
                Rate = rate
            };

            Apply(@event);
            Append(@event);
        }

        public void RecordInflow(DateTime timeStamp, double hours, double? additionalRate)
        {
            var @event = new NewInflowRecorded(Id, new InFlow(timeStamp, hours, additionalRate));
            Apply(@event);
            Append(@event);
        }

        public void RecordOutflow(DateTime timeStamp, double hours)
        {
            var @event = new NewOutflowRecorded(Id, new OutFlow(timeStamp, hours));
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
                TotalHour += @event.Inflow.Hours;
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
            TotalHour -= @event.OutFlow.Hours;
        }


        public void RateChange(DateTime timeStamp, double value)
        {
            var @event = new RateChanged()
                { RateId = Id, TimeStamp = timeStamp, Value = value };
            Apply(@event);
            Append(@event);

        }

        public void Apply(RateChanged @event)
        {
            Rate = @event.Value;
        }
    }
}
