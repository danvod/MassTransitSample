namespace Messaging.Contracts.Alert;

/// <summary>
/// A message consumed by the Alert.AlertConsumer
///
/// Messages are produced by both Project1 and Project2
/// </summary>
public class AlertEvent
{
    public string Origin { get; set; }

    public string Text { get; set; }
}