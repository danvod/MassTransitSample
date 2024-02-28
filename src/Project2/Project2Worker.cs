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
            /*
             * Alternative way to Publish, compared to Project1Worker
             * This enables passing a custom header
             */
            await _bus.Publish<AlertEvent>(new
            {
                Origin = "Project2",
                Text = "AlertEvent message from Project2 Worker",
                __Header_My_Custom_Header = "just-a-sample"
            });
            await Task.Delay(5000, stoppingToken);
        }
    }
}