using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Moonman.Function
{
    public class AzureTopicTriggerFunction1
    {
        private readonly ILogger<AzureTopicTriggerFunction1> _logger;

        public AzureTopicTriggerFunction1(ILogger<AzureTopicTriggerFunction1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(AzureTopicTriggerFunction1))]
        public async Task Run(
            [ServiceBusTrigger("topic-dystopia-0", "sub0", Connection = "moonbus0_SERVICEBUS")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

             // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
