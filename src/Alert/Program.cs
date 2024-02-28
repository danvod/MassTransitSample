using Alert;
using Alert.Consumers;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                 {
                     services.AddMassTransit(x =>
                     {
                         /*
                          * Sufficient configuration for this use case.
                          * AlertConsumer will process AlertEvent from any origin. 
                          */
                         x.AddConsumer<AlertConsumer>();
                         
                         x.UsingRabbitMq((context,cfg) =>
                         {
                             cfg.Host("localhost", "/", h => {
                                 h.Username("guest");
                                 h.Password("guest");
                             });
                             
                             cfg.ConfigureEndpoints(context);
                         });
                     });
                     
                     services.AddHostedService<Worker>();
                 })
                 .Build();

await host.RunAsync();