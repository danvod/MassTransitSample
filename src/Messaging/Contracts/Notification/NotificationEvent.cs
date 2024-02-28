namespace Messaging.Contracts.Notification;

/// <summary>
/// A message with 2 consumers in 2 separate projects/services
/// - Project1.NotificationConsumer
/// - Project2.NotificationConsumer
///
/// Each consumer receives an exact copy of this message.
/// </summary>
public class NotificationEvent
{
    public Guid Value { get; set; }
}