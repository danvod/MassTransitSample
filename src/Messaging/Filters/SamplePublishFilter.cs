using MassTransit;
using Microsoft.Extensions.Logging;
using Service;

namespace Messaging.Filters;

public class SamplePublishFilter<T> :
    IFilter<PublishContext<T>>
    where T : class
{
    private readonly IIdentityService _identity;

    public SamplePublishFilter(IIdentityService identity)
    {
        _identity = identity;
    }

    public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        //  Generate a dummy token and set it so we can pass its value with the message
        var tokenVal = await _identity.GetToken();
        _identity.SetToken(tokenVal);

        var token = _identity.Token;

        context.Headers.Set("Token", token);

        Console.WriteLine("Publish filter - set token value: {0}", token);

        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}