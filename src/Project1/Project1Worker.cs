using MassTransit;
using Messaging.Contracts.Alert;

namespace Project1;

public class Project1Worker : BackgroundService
{
    private readonly IBus _bus;

    public Project1Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var alert = new AlertEvent
            {
                Origin = "Project1",
                Text = "AlertEvent message from Project1 Worker"
            };

            await _bus.Publish(alert);

            await Task.Delay(3000, stoppingToken);
        }
    }
}