using MassTransit;
using Messaging.Contracts.Alert;

namespace Project2;

public class Project2Worker : BackgroundService
{
    private readonly IBus _bus;

    private readonly ILogger<Project2Worker> _logger;

    public Project2Worker(ILogger<Project2Worker> logger, IBus bus)
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
                Origin = "Project2",
                Text = $"{nameof(AlertEvent)} message from Project2 Worker"
            };

            await _bus.Publish(alert);
            await Task.Delay(5000, stoppingToken);
        }
    }
}