using MassTransit;
using Messaging.Contracts.Notification;

namespace Project2.Consumers;

/// <summary>
/// Control all of the definitions about a consumer and its associated endpoint.
///
/// This is an alternative way to configure, compared to
/// cfg.ReceiveEndpoint("notification-p2", e => {});
/// </summary>
public class NotificationConsumerDefinition : ConsumerDefinition<NotificationConsumer>
{
    public NotificationConsumerDefinition()
    {
        EndpointName = "notification-p2";
    }
}

public class NotificationConsumer : IConsumer<NotificationEvent>
{
    readonly ILogger<NotificationEvent> _logger;

    public NotificationConsumer(ILogger<NotificationEvent> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<NotificationEvent> context)
    {
        _logger.LogInformation("Handling {0} in Project2, Value: {1}", nameof(NotificationEvent),
            context.Message.Value);
        return Task.CompletedTask;
    }
}