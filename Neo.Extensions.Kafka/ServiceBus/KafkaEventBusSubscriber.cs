using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Logging;
using Neo.Extensions.Core;
using Neo.Extensions.Kafka.Abstractions;

namespace Neo.Extensions.Kafka.ServiceBus
{
    public class KafkaEventBusSubscriber : IEventBusSubscriber
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger _logger;
        private readonly IEventProvider _eventProvider;
        private readonly IMediator _mediator;

        public KafkaEventBusSubscriber(IConsumer<string, string> consumer, ILogger<KafkaEventBusSubscriber> logger,
            IEventProvider eventProvider, IMediator mediator)
        {
            _consumer = consumer;
            _logger = logger;
            _eventProvider = eventProvider;
            _mediator = mediator;
        }

        [Obsolete]
        public async Task SubscribeEventAsync(string topicName, CancellationToken cancellationToken)
        {
            using var consumer = _consumer;
            consumer.Subscribe(topicName);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await ConsumeNextEvent(consumer, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error consuming message: {e.Message} {e.StackTrace}");
                consumer.Close();
            }
        }

        /// <summary>
        /// Permite que um aplicativo se inscreva em um tópico e processe o fluxo de registros produzidos para eles.
        /// </summary>
        /// <param name="topic">Tópico</param>
        /// <param name="prefixEnv">Prefixo que será concatenado ao nome do tópico</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns></returns>
        public async Task SubscribeEventAsync(Topics.EnumTopics topic, string prefixEnv, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(prefixEnv))
            {
                throw new ArgumentNullException(nameof(prefixEnv));
            }

            var topicName = string.Concat(prefixEnv, "-", topic.GetEnumDescription());
            using var consumer = _consumer;
            consumer.Subscribe(topicName);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await ConsumeNextEvent(consumer, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error consuming message: {e.Message} {e.StackTrace}");
                consumer.Close();
            }
        }
        private async Task ConsumeNextEvent(IConsumer<string, string> consumer, CancellationToken cancellationToken)
        {
            try
            {
                var message = consumer.Consume(cancellationToken);
                if (message.Message == null) return;

                _logger.LogInformation(message: $"Consuming message key: {message?.Message?.Key} - value { message?.Message?.Value } - Topic: { message?.Topic }");
                //var eventType = _eventProvider.GetByKey(message.Key);
                //var @event = JsonSerializer.Deserialize(message?.Value, eventType);
                //if (@event == null) return;

                //await _mediator.Publish(@event, cancellationToken)
                //    .ConfigureAwait(false);
                await Task.CompletedTask;

                consumer.Commit();
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error consuming message: {e.Message} {e.StackTrace}");
            }
        }
    }
}
