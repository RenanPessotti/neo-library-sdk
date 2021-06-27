using System.Threading.Tasks;

namespace Neo.Extensions.RabbitMq.Bus
{
    public interface IBusPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
