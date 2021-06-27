using MediatR;

namespace Neo.Extensions.Kafka.Abstractions
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IEvent
    {
    }
}
