using MassTransit;
using Messaging.Contracts.KillSwitch;

namespace Project1.Consumers;

public class KillSwitchConsumer : IConsumer<KillSwitchEvent>
{
    readonly ILogger<KillSwitchConsumer> _logger;

    public KillSwitchConsumer(ILogger<KillSwitchConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<KillSwitchEvent> context)
    {
        _logger.LogInformation("KillSwitch Sample - Value {0}", context.Message.Value);

        if (context.Message.Value < 10)
        {
            throw new Exception("Bad things");
        }

        _logger.LogInformation("KillSwitch Sample - Value {0} processed...", context.Message.Value);
        return Task.CompletedTask;
    }
}

public class KillSwitchConsumerDefinition : ConsumerDefinition<KillSwitchConsumer>
{
    public KillSwitchConsumerDefinition()
    {
    }
    
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<KillSwitchConsumer> consumerConfigurator)
    {
        /*
         * the kill switch will activate after 5 messages have been consumed.
         * If the ratio of failures/attempts exceeds 15%, the kill switch will trip and stop the receive endpoint.
         * After 1 minute, the receive endpoint will be restarted.
         * Once restarted, if exceptions are still observed, the receive endpoint will be stopped again for 1 minute.
         */
        endpointConfigurator.UseKillSwitch(options => options
                                                     .SetActivationThreshold(5)
                                                     .SetTripThreshold(0.15)
                                                     .SetRestartTimeout(m: 1));
    }
}