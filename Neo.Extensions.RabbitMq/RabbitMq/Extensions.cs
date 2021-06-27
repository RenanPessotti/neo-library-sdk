using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Enrichers.MessageContext.Context;
using RawRabbit.Instantiation;
using Neo.Extensions.RabbitMq.Bus;

namespace Neo.Extensions.RabbitMq.RabbitMq
{
    public static class Extensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RawRabbitConfiguration();
            configuration.GetSection("RabbitMq").Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options,
                Plugins = p => p
                    .UseGlobalExecutionId()
                    .UseHttpContext()
                    .UseMessageContext(c => new MessageContext { GlobalRequestId = Guid.NewGuid() })
            });
            services.AddSingleton<IBusClient>(_ => client);

            services.AddScoped<IBusPublisher, BusPublisher>();

            return services;
        }

        public static IApplicationBuilder UseRabbit(this IApplicationBuilder app)
        {
            //app.UseRabbitMq().SubscribeEvent<QuizCreatedEvent>();

            return app;
        }

        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app) => new BusSubscriber(app);
        public static IBusSubscriber UseRabbitMq(this IServiceProvider app) => new BusSubscriber(app);
    }
}
