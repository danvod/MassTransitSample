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
        //  Custom header will only be visible from Project2Worker
        _logger.LogInformation("Received Alert From: {0}, Value: {1}, Custom Header: {2}",
            context.Message.Origin, context.Message.Text, context.Headers.Get<string>("My-Custom-Header"));
        return Task.CompletedTask;
    }
}