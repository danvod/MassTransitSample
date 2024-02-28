using MassTransit;
using Messaging.Contracts.Alert;

namespace Project1;

public class Project1Worker : BackgroundService
{
    private readonly ILogger<Project1Worker> _logger;
    private readonly IBus _bus;
    public Project1Worker(ILogger<Project1Worker> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var alert = new AlertEvent()
            {
                Origin = "Project1",
                Text = $"{nameof(AlertEvent)} message from Project1 Worker"
            };

            await _bus.Publish(alert);
            
            await Task.Delay(3000, stoppingToken);
        }
    }

    public async Task DispatchAlerts()
    {
        var alert = new AlertEvent()
        {
            Origin = "Project1",
            Text = "P1"
        };

        await _bus.Publish(alert);
    }
}