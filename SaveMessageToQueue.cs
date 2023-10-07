using System;
using System.Threading.Tasks;
using AzureKafkaTriggerFun.MessageQueueClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureKafkaTriggerFun
{
    public class SaveMessageToQueue
    {
        private readonly IMessageQueue _messageQueue;
        private readonly FuncConfig _configuration;
        public SaveMessageToQueue(IMessageQueue messageQueue, IOptions<FuncConfig> configuration)
        {
            _messageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
            _configuration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Runs the specified events.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="log">The log.</param>
        [FunctionName("SaveMessageToQueueFunc")]
        public async Task Run(
            [KafkaTrigger("localhost:9092",
                          "kafka-topic-demo",
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            ILogger log)
        {
            foreach (KafkaEventData<string> eventData in events)
            {
                await _messageQueue.Send(_configuration.QueueName, eventData.Value);
                log.LogInformation($"C# Kafka trigger function processed a message: {eventData.Value}");
            }
        }
    }
}