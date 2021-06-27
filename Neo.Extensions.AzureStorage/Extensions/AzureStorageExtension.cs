using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace Neo.Extensions.AzureStorage.Extensions
{
    public static class AzureStorageExtension
    {
        public static void ConfigureAzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAzureStorageService, AzureStorageService>();

            services.AddSingleton<CloudStorageAccount>(factory =>
                CloudStorageAccount.Parse(configuration.GetConnectionString("AzureStorageAccount")));

            services.AddSingleton<CloudQueueClient>(factory =>
                factory.GetService<CloudStorageAccount>().CreateCloudQueueClient());

            services.AddSingleton<CloudBlobClient>(factory =>
                factory.GetService<CloudStorageAccount>().CreateCloudBlobClient());

            services.AddSingleton<CloudTableClient>(factory =>
                factory.GetService<CloudStorageAccount>().CreateCloudTableClient());
        }
    }
}
