using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Neo.Extensions.AzureStorage
{
    public interface IAzureStorageService
    {
        Task AdicionarFila(object obj, string queue, TimeSpan? timeToLive = null, bool esperarTerminar = true);
        Task<CloudQueueMessage> AdicionarFila(object obj, string queue, TimeSpan initialVisibilityDelay);
        Task RemoverFila(CloudQueueMessage message, string queue);
        Task RemoverFila(string messageId, string popReceipt, string queue);        
    }
}
