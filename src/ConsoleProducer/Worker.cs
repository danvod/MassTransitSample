using MassTransit;
using Messaging.Contracts.Direct;
using Messaging.Contracts.Notification;

namespace ConsoleProducer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var args = Environment.GetCommandLineArgs();
        var sampleToRun = args[1];

        _logger.LogInformation("Running Example {0}", sampleToRun);

        while (!stoppingToken.IsCancellationRequested)
        {
            var g = Guid.NewGuid();
            
            switch (sampleToRun)
            {
                case "NotificationEvent":
                    _logger.LogInformation("Sending {0}", g);
                    await _bus.Publish(new NotificationEvent
                    {
                        Value = g
                    });
                    break;
                case "RoundRobinEvent":
                    _logger.LogInformation("Sending {0}", g);
                    await _bus.Publish(new RoundRobinEvent()
                    {
                        Value = g
                    });
                    break;
                case "DirectA":
                    await _bus.Publish(new DirectEvent()
                    {
                        Value = "Publish - Direct event from ConsoleProducer A"
                    }, x => x.SetRoutingKey("A"));
                    
                    // It is also possible to Send directly to a queue or an exchange
                    var endpoint = await _bus.GetSendEndpoint(new Uri("exchange:direct-a"));

                    await endpoint.Send(new DirectEvent
                    {
                        Value = "Send - Direct event from ConsoleProducer A"
                    }, x => x.SetRoutingKey("A"));
                    
                    break;
                case "DirectB":
                    await _bus.Publish(new DirectEvent()
                    {
                        Value = "Direct event from ConsoleProducer B"
                    }, x => x.SetRoutingKey("B"));
                    break;
            }

            await Task.Delay(2500, stoppingToken);
        }
    }
}