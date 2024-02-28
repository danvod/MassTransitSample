using System.Security.Principal;
using MassTransit;
using ConsoleProducer;
using Messaging.Contracts.Direct;
using Messaging.Filters;
using RabbitMQ.Client;
using Service;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                 {
                     services.AddScoped<IIdentityService, IdentityService>();
                     
                     services.AddMassTransit(x =>
                     {
                         x.UsingRabbitMq((context, cfg) =>
                         {
                             cfg.Host("localhost", "/", h =>
                             {
                                 h.Username("guest");
                                 h.Password("guest");
                             });
                             
                             // Configure the producer side of DirectEvent
                             // The default is "fanout", so both sides have to be configured as direct in this example
                             cfg.Publish<DirectEvent>(m =>
                             {
                                 m.ExchangeType = ExchangeType.Direct;
                             });
                             
                             cfg.UsePublishFilter(typeof(SamplePublishFilter<>), context);
                             
                             cfg.ConfigureEndpoints(context);
                         });
                     });

                     services.AddHostedService<Worker>();
                 })
                 .Build();

await host.RunAsync();