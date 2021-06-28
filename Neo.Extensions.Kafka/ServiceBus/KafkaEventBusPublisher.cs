using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Neo.Extensions.Core;
using Neo.Extensions.Kafka.Abstractions;

namespace Neo.Extensions.Kafka.ServiceBus
{
    public class KafkaEventBusPublisher : IEventBusPublisher
    {
        private readonly IProducer<string, string> _producer;

        public KafkaEventBusPublisher(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        [Obsolete]
        public async Task PublishAsync<TEvent>(TEvent @event, string topicName) where TEvent : IEvent
        {
            var data = JsonSerializer.Serialize(@event);
            var message = new Message<string, string>
            {
                Key = @event.GetType().Name,
                Value = data
            };

            await _producer.ProduceAsync(topicName, message)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Permite a publicação de mensagens em um tópico
        /// </summary>
        /// <typeparam name="TEvent">Tipo IEvent</typeparam>
        /// <param name="event">Mensagem que será enviada</param>
        /// <param name="topic">Tópico que será publicada a mensagem</param>
        /// <param name="prefixEnv">Prefixo que será concatenado ao nome do tópico.</param>
        /// <returns></returns>
        public async Task PublishAsync<TEvent>(TEvent @event, Topics.EnumTopics topic, string prefixEnv) where TEvent : IEvent
        {
            if (string.IsNullOrEmpty(prefixEnv))
            {
                throw new ArgumentNullException(nameof(prefixEnv));
            }

            var data = JsonSerializer.Serialize(@event);
            var message = new Message<string, string>
            {
                Key = @event.GetType().Name,
                Value = data
            };
                       
            var topicName = string.Concat(prefixEnv, "-", topic.GetEnumDescription());

            await _producer.ProduceAsync(topicName, message)
                .ConfigureAwait(false);
        }
    }
}
