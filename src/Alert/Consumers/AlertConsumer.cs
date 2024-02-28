using MassTransit;
using Messaging.Contracts.Alert;

namespace Alert.Consumers;

public class AlertConsumer :
    IConsumer<AlertEvent>
{
    readonly ILogger<AlertConsumer> _logger;

    public AlertConsumer(ILogger<AlertConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<AlertEvent> context)
    {
        _logger.LogInformation("Received Alert From: {Text}, Value: {Value}", context.Message.Origin, context.Message.Text);
        return Task.CompletedTask;
    }
}