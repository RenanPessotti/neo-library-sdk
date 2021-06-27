using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Neo.Extensions.AzureStorage
{
    internal class AzureStorageService : IAzureStorageService
    {
        private readonly CloudBlobClient _blobClient;
        private readonly CloudQueueClient _queueClient;
        private readonly CloudTableClient _tableClient;

        public AzureStorageService(CloudBlobClient blobClient,
            CloudQueueClient queueClient,
            CloudTableClient tableClient)
        {
            _blobClient = blobClient;
            _queueClient = queueClient;
            _tableClient = tableClient;
        }

        public async Task AdicionarFila(object obj, string queue, TimeSpan? timeToLive = null, bool esperarTerminar = true)
        {
            var fila = _queueClient.GetQueueReference(queue);

            if (!(await fila.ExistsAsync()))
                await fila.CreateIfNotExistsAsync();

            string objQueue = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            if (esperarTerminar)
            {
                await fila.AddMessageAsync(new CloudQueueMessage(objQueue), timeToLive, null, null, null);
            }
            else
            {
                using (var cts = new CancellationTokenSource())
                {
                    cts.CancelAfter(5000);
                    await fila.AddMessageAsync(new CloudQueueMessage(objQueue), timeToLive, null, null, null, cts.Token);
                }
            }
        }

        public async Task<CloudQueueMessage> AdicionarFila(object obj, string queue, TimeSpan initialVisibilityDelay)
        {
            var fila = _queueClient.GetQueueReference(queue);

            string objQueue = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var message = new CloudQueueMessage(objQueue);

            if (!(await fila.ExistsAsync()))
                await fila.CreateIfNotExistsAsync();

            await fila.AddMessageAsync(message, null, initialVisibilityDelay, null, null);
           
            return message;
        }

        public async Task RemoverFila(CloudQueueMessage message, string queue)
        {
            try
            {
                var fila = _queueClient.GetQueueReference(queue);

                if (!(await fila.ExistsAsync()))
                    await fila.CreateIfNotExistsAsync();

                await fila.DeleteMessageAsync(message);
            }
            catch (Exception) { }
        }

        public async Task RemoverFila(string messageId, string popReceipt, string queue)
        {
            try
            {
                var fila = _queueClient.GetQueueReference(queue);

                if (!(await fila.ExistsAsync()))
                    await fila.CreateIfNotExistsAsync();

                await fila.DeleteMessageAsync(messageId, popReceipt);
            }
            catch (Exception) { }
        }        
    }
}
