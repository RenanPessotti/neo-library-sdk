using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Neo.Extensions.Transaction.Extensions
{
    public static class TransactionExtension
    {
        public static void ConfigureTransaction<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            services.AddTransient<ITransactionScopeProvider>(factory => new TransactionScopeProvider(factory.GetService<T>()));
        }
    }
}
