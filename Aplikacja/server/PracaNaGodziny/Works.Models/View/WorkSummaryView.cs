using System;
using System.Collections.Generic;
using System.Text;
using Marten.Events;
using Works.Shared.Events;

namespace Works.Models.View
{
    public class WorkSummaryView
    {
        public Guid Id { get; set; }
        public Guid WorkerId { get; set; }
        public Guid WorkId { get; set; }
        public Guid LocationId { get; set; }

        public double Rate { get; set; }
        public decimal Wage { get; set; }

        public double TotalHour { get; set; }
        public double PaidHour { get; set; }
        public double AdditionalHour { get; set; }
        public decimal AdditionalWage { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        //stats
        public WorkStat WorkStat { get; set; }

        public WorkSummaryView()
        {
            WorkStat = new WorkStat();
        }

        public void Apply(NewWorkCreated @event)
        {
            Id = @event.WorkId;
            WorkId = @event.WorkId;
            LocationId = @event.LocationId;
            WorkerId = @event.WorkerId;
            Rate = @event.Rate;
        }

        public void Apply(NewInflowRecorded @event)
        {
            if (!@event.Inflow.AdditionalRate.HasValue || @event.Inflow.AdditionalRate.Value <=0)
            {
                var wage = Convert.ToDecimal(@event.Inflow.Hours * Rate);
                Wage += wage;
                TotalHour += @event.Inflow.Hours;
                WorkStat.Add(@event.Inflow.TimeStamp, wage, @event.Inflow.Hours);
            }
            else
            {
                var additionalWage = Convert.ToDecimal(@event.Inflow.Hours * (Rate + @event.Inflow.AdditionalRate.Value));
                AdditionalWage += additionalWage;
                AdditionalHour += @event.Inflow.Hours;
                WorkStat.Add(@event.Inflow.TimeStamp, additionalWage, @event.Inflow.Hours);
            }
        }

        public void Apply(IEvent @event)
        {
            if (@event.Data.GetType() == typeof(NewInflowRecorded))
            {
                var eventToApply = (NewInflowRecorded)@event.Data;

                if (From.HasValue && To.HasValue && eventToApply.Inflow.TimeStamp > From && eventToApply.Inflow.TimeStamp < To)
                    Apply(eventToApply);
                if(From.HasValue && To == null && eventToApply.Inflow.TimeStamp > From)
                    Apply(eventToApply);
                if (From == null && To.HasValue && eventToApply.Inflow.TimeStamp < To)
                    Apply(eventToApply);
                if(From == null && To == null)
                    Apply(eventToApply);

            }

            if (@event.Data.GetType() == typeof(NewOutflowRecorded))
            {
                var eventToApply = (NewOutflowRecorded)@event.Data;

                if (From.HasValue && To.HasValue && eventToApply.OutFlow.TimeStamp > From && eventToApply.OutFlow.TimeStamp < To)
                    Apply(eventToApply);
                if (From.HasValue && To == null && eventToApply.OutFlow.TimeStamp > From)
                    Apply(eventToApply);
                if (From == null && To.HasValue && eventToApply.OutFlow.TimeStamp < To)
                    Apply(eventToApply);
                if (From == null && To == null)
                    Apply(eventToApply);
            }

            if (@event.Data.GetType() == typeof(NewWorkCreated))
            {
                Apply((NewWorkCreated)@event.Data);
            }
        }

        public void Apply(NewOutflowRecorded @event)
        {
            var wage = Convert.ToDecimal(@event.OutFlow.Hours * Rate);
            Wage -= wage;
            PaidHour += @event.OutFlow.Hours;
            WorkStat.Substract(@event.OutFlow.TimeStamp, wage, @event.OutFlow.Hours);
        }
    }
}
