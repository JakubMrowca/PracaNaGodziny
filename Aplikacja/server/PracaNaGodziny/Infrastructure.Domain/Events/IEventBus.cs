using System.Threading.Tasks;

namespace Infrastructure.Domain.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}