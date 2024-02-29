using MassTransit;
using Messaging.Contracts.Notification;

namespace Project1.Consumers;

public class RoundRobinConsumer : IConsumer<RoundRobinEvent>
{
    readonly ILogger<RoundRobinConsumer> _logger;

    public RoundRobinConsumer(ILogger<RoundRobinConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<RoundRobinEvent> context)
    {
        _logger.LogInformation("Handling message from: {0}, Value: {1}", nameof(RoundRobinConsumer), context.Message.Value);
        return Task.CompletedTask;
    }
}