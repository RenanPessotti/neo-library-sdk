using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration.Exchange;
using Serilog;
using Neo.Extensions.RabbitMq.Bus;

namespace Neo.Extensions.RabbitMq.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;
        
        public BusSubscriber(IServiceProvider app)
        {
            _serviceProvider = app.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
        }

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
        }

        public IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest
        {
            _busClient.SubscribeAsync<TEvent>(async (@event) =>
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var handler = scope.ServiceProvider.GetService<IMediator>();
                        await handler.Send(@event);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Erro ao processar mensagem");
                    throw;
                }

            }, ctx => ctx.UseSubscribeConfiguration(cfg => cfg
                .Consume(c => c.WithRoutingKey(typeof(TEvent).Name))
                .FromDeclaredQueue(q => q
                    .WithName(GetQueueName<TEvent>())
                    .WithDurability()
                    .WithAutoDelete(false))
                .OnDeclaredExchange(e => e
                  .WithName("sample-rabbitmq-publish")
                  .WithType(ExchangeType.Topic)
                  .WithArgument("key", typeof(TEvent).Name.ToLower()))
            ));
            return this;
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";
    }
}
