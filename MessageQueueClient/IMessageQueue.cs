using System.Threading.Tasks;

namespace AzureKafkaTriggerFun.MessageQueueClient
{
    public interface IMessageQueue
    {
        /// <summary>
        /// Sends the specified queue name.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task Send(string queueName, string message);
    }
}