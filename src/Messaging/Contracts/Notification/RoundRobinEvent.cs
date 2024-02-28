namespace Messaging.Contracts.Notification;

/// <summary>
/// A message with 2 consumers in 2 separate projects/services.
///
/// Different from <see cref="NotificationEvent"/> as there are no separate queues declared for the consumers.
/// 
/// Project1.RoundRobinConsumer and Project2.RoundRobinConsumer will consume this message in a round-robin manner
/// </summary>
public class RoundRobinEvent
{
    public Guid Value { get; set; }
}