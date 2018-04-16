using Marten.Events.Projections;
using System;
using System.Collections.Generic;
using System.Text;
using Marten;
using Works.Shared.Events;

namespace Works.Models.View
{
    public class WorkSummaryViewProjection: ViewProjection<WorkSummaryView, Guid>
    {
        public WorkSummaryViewProjection()
    {
        ProjectEvent<NewWorkCreated>(e => e.WorkId, (w, @event) => w.Apply(@event));
        ProjectEvent<NewInflowRecorded>((ev) => ev.ForWorkId, (view, @event) => view.Apply(@event));
        
        ProjectEvent<NewOutflowRecorded>((ev) => ev.ForWorkId, (view, @event) => view.Apply(@event));
    }

}
}
