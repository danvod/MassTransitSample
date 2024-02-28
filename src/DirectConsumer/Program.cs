using System.Reflection;
using DirectConsumer;
using MassTransit;
using Messaging.Contracts.Direct;
using RabbitMQ.Client;
using DirectConsumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                 {
                     services.AddMassTransit(x =>
                     {
                         var consumerAssembly = Assembly.GetAssembly(typeof(DirectEventConsumer));
                         x.AddConsumers(consumerAssembly);
                         
                         x.UsingRabbitMq((context,cfg) =>
                         {
                             cfg.Host("localhost", "/", h => {
                                 h.Username("guest");
                                 h.Password("guest");
                             });
                             
                             /*
                              * This creates
                              * 1) Exchange: Messaging.Contracts.DirectMessage:DirectEvent
                              * 2) Exchange: direct-p1
                              * 3) Queue: direct-p1
                              *
                              * DirectMessage:DirectEvent is bound to
                              * Exchange: direct-p1, which is bound to Queue: direct-p1
                              */
                             cfg.ReceiveEndpoint("direct-a", e =>
                             {
                                 e.ConfigureConsumeTopology = false;
                                 e.Bind<DirectEvent>(b =>
                                 {
                                     b.ExchangeType = ExchangeType.Direct;
                                     b.RoutingKey = "A";
                                 });
                                 
                                 e.ConfigureConsumer<DirectEventConsumer>(context);
                             });
                             
                             cfg.ReceiveEndpoint("direct-b", e =>
                             {
                                 e.ConfigureConsumeTopology = false;
                                 e.Bind<DirectEvent>(b =>
                                 {
                                     b.ExchangeType = ExchangeType.Direct;
                                     b.RoutingKey = "B";
                                 });
                                 
                                 // Note - this could be an entirely different consumer
                                 e.ConfigureConsumer<DirectEventConsumer>(context);
                             });
                         });
                     });

                     
                     services.AddHostedService<Worker>();
                 })
                 .Build();

await host.RunAsync();