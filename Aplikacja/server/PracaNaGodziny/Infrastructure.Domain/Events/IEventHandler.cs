using MediatR;

namespace Infrastructure.Domain.Events
{
    public interface IEventHandler<in TEvent>: INotificationHandler<TEvent>
           where TEvent : IEvent
    {
    }
}
