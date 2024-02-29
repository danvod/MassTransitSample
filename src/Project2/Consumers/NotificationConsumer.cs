using MassTransit;
using Messaging.Contracts.Notification;
using Service;

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
    readonly ILogger<NotificationConsumer> _logger;
    private readonly IIdentityService _identity;

    public NotificationConsumer(ILogger<NotificationConsumer> logger, IIdentityService identity)
    {
        _logger = logger;
        _identity = identity;
    }

    public Task Consume(ConsumeContext<NotificationEvent> context)
    {
        _logger.LogInformation("Handling {0} in Project2, Value: {1}, Token: {2}", nameof(NotificationEvent),
            context.Message.Value,
            _identity.Token);
        return Task.CompletedTask;
    }
}