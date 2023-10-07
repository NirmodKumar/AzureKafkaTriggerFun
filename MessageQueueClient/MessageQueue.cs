using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;

namespace AzureKafkaTriggerFun.MessageQueueClient
{
    public class MessageQueue : IMessageQueue
    {
        private readonly FuncConfig _configuration;

        public MessageQueue(IOptions<FuncConfig> configuration)
        {
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task Send(string queueName, string message)
        {
            var queueClient = new QueueClient(_configuration.StorageAccountConnectionString, queueName);

            await queueClient.SendMessageAsync(message);
        }
    }
}