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
        var token = context.Headers.Get<string>("Token");
        
        _identity.SetToken(token);

        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}