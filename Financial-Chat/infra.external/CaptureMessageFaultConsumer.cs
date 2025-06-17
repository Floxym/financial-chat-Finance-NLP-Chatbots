using Financial.Chat.Domain.Shared.Entity;
using Financial.Chat.Domain.Shared.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker.Consumers
{
    public class CaptureMessageFaultConsumer : IConsumer<Fault<MessageDto>>
    {
        private readonly ILogger<CaptureMessageFaultConsumer> _logger;

        public CaptureMessageFaultConsumer(ILogger<CaptureMessageFaultConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Fault<MessageDto>> context)
        {
            _logger.LogInformation($"FAULT: Message received: {context.Message}");
            await Task.CompletedTask;
        }
    }
}
