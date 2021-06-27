using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.Configuration.Exchange;
using RawRabbit.Enrichers.MessageContext;
using Neo.Extensions.RabbitMq.Bus;

namespace Neo.Extensions.RabbitMq.RabbitMq
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;

        public BusPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            await _busClient.PublishAsync(@event, ctx => ctx
                .UsePublishConfiguration(cfg => cfg 
                    .OnDeclaredExchange(e => e
                        .WithName("sample-rabbitmq-publish")
                        .WithType(ExchangeType.Topic))
                    .WithRoutingKey(typeof(TEvent).Name)));
        }
    }
}
