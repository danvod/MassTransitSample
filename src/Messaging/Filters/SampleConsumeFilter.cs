using MassTransit;
using Service;

namespace Messaging.Filters;

public class SampleConsumeFilter<T> :
    IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly IIdentityService _identity;
    public SampleConsumeFilter(IIdentityService identity)
    {
        _identity = identity;
    }

    public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        Console.WriteLine("In Sample Consume filter");

        var token = context.Headers.Get<string>("Token");

        Console.WriteLine("Consume Filter - Received token value: {0}", token);
        _identity.SetToken(token);

        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}