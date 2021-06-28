using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Neo.Extensions.Persistence.Context;

namespace Neo.Extensions.LogSdk
{
    public static class NeoLogSdk
    {
        public static IServiceCollection AddSDKContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SDKDbContext>(options =>
                   options.UseSqlServer(connectionString)
                       .UseLazyLoadingProxies());

            services.AddTransient<ILogEmailService, LogEmailService>();
            services.AddTransient<ILogEmailRepository, LogEmailRepository>();

            return services;
        }

    }
}
