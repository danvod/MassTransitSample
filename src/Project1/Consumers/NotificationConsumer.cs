using System.Security.Principal;
using MassTransit;
using Messaging.Contracts.Notification;
using Service;

namespace Project1.Consumers;

public class NotificationConsumer : IConsumer<NotificationEvent>
{
    readonly ILogger<NotificationEvent> _logger;
    private readonly IIdentityService _identity;

    public NotificationConsumer(ILogger<NotificationEvent> logger, IIdentityService identity)
    {
        _logger = logger;
        _identity = identity;
    }

    public Task Consume(ConsumeContext<NotificationEvent> context)
    {
        _logger.LogInformation("Handling {0} in Project1, Value: {1}, Token: {2}", nameof(NotificationEvent),
            context.Message.Value,
            context.Headers.Get<string>("Token"));
        
        _logger.LogInformation("{0}, Token Value: {1}", nameof(NotificationEvent), _identity.Token);
        
        return Task.CompletedTask;
    }
}