using System;
using System.Linq;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo.Extensions.Kafka.Abstractions;
using Neo.Extensions.Kafka.ServiceBus;

namespace Neo.Extensions.Kafka.Extensions
{
    public static class KafkaExtension
    {
        public static void AddKafkaEventBusSubscriber(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var events = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(x => typeof(IEvent).IsAssignableFrom(x) && !x.IsInterface))
                .Select(x => ((string, Type))(x.Name, x))
                .ToArray();

            var eventProvider = new EventProvider();
            eventProvider.RegisterEvent(events);

            var consumerConfig = new ConsumerConfig();
            configuration.Bind("consumer", consumerConfig);
            var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

            serviceCollection.AddSingleton(consumer);
            serviceCollection.AddSingleton<IEventProvider>(eventProvider);
            serviceCollection.AddTransient<IEventBusSubscriber, KafkaEventBusSubscriber>();
        }

        public static void AddKafkaEventBusPublisher(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();

            configuration.Bind("producer", producerConfig);
            var producer = new ProducerBuilder<string, string>(producerConfig).Build();

            serviceCollection.AddSingleton(producer);
            serviceCollection.AddTransient<IEventBusPublisher, KafkaEventBusPublisher>();
        }
    }
}
