using MassTransit;
using Messaging.Contracts.Direct;
using RabbitMQ.Client;

namespace SendConsumer.Consumers;

public class DirectEventConsumer : IConsumer<DirectEvent>
{
    readonly ILogger<DirectEvent> _logger;

    public DirectEventConsumer(ILogger<DirectEvent> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<DirectEvent> context)
    {
        _logger.LogInformation("Handling message from: {0}, Value: {1}, RoutingKey: {2}", 
            nameof(DirectEventConsumer),
            context.Message.Value,
            context.RoutingKey());
        return Task.CompletedTask;
    }
}