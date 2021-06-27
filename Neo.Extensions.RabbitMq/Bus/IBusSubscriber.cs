using MediatR;

namespace Neo.Extensions.RabbitMq.Bus
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest;
    }
}
