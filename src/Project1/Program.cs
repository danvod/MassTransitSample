using System.Reflection;
using System.Security.Principal;
using MassTransit;
using Messaging.Filters;
using Project1;
using Project1.Consumers;
using Service;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                 {
                     services.AddScoped<IIdentityService, IdentityService>();
                     
                     services.AddMassTransit(x =>
                     {
                         var consumerAssembly = Assembly.GetAssembly(typeof(RoundRobinConsumer));

                         // Register consumers from a given assembly
                         //x.AddConsumers(consumerAssembly);

                         // Manually register a consumer
                         // x.AddConsumer<RoundRobinConsumer>();

                         /*
                          * This creates
                          * 1) Exchange: Messaging.Contracts.Notification:RoundRobinEvent
                          * 2) Exchange: RoundRobin
                          * 3) Queue: RoundRobin
                          *
                          * Messaging.Contracts.Notification.RoundRobinEvent is bound to
                          * Exchange: RoundRobin which is bound to Queue: RoundRobin
                          *
                          * RoundRobinConsumer in Project2 will have the same configuration,
                          * which means the consumer in Project1 and Project2 will handle RoundRobinEvent in a round-robin manner
                          */
                         x.AddConsumer<RoundRobinConsumer>();
                         x.AddConsumer<NotificationConsumer>();

                         // Note - Consumers are added before adding the transport
                         x.UsingRabbitMq((context, cfg) =>
                         {
                             cfg.Host("localhost", "/", h =>
                             {
                                 h.Username("guest");
                                 h.Password("guest");
                             });

                             /*
                              * This creates
                              * 1) Exchange: Messaging.Contracts.Notification.NotificationEvent
                              * 2) Exchange: notification-p1
                              * 3) Queue: notification-p1
                              *
                              * Messaging.Contracts.Notification.NotificationEvent is bound to
                              * Exchange: notification-p1, which is bound to Queue: notification-p1
                              *
                              * Because of configuration in Project2
                              * Exchange: Messaging.Contracts.Notification.NotificationEvent is also bound to
                              * Exchange: notification-p2
                              */
                             cfg.ReceiveEndpoint("notification-p1",
                                 e => { e.ConfigureConsumer<NotificationConsumer>(context); });

                             cfg.UseConsumeFilter(typeof(SampleConsumeFilter<>), context);
                             
                             // When configuring endpoints manually, ConfigureEndpoints should be excluded or be called after any explicitly configured receive endpoints.
                             cfg.ConfigureEndpoints(context);
                         });
                     });

                     services.AddHostedService<Project1Worker>();
                 })
                 .Build();

await host.RunAsync();