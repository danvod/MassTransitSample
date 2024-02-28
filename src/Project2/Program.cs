using System.Reflection;
using MassTransit;
using Project2;
using Project2.Consumers;
using Service;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                 {
                     services.AddScoped<IIdentityService, IdentityService>();
                     
                     services.AddMassTransit(x =>
                     {
                         var consumerAssembly = Assembly.GetAssembly(typeof(RoundRobinConsumer));
                         x.AddConsumers(consumerAssembly);
                         
                         // Alternatively, add Consumer with its definition
                         //x.AddConsumer<NotificationConsumer, NotificationConsumerDefinition>();
                         
                         x.UsingRabbitMq((context,cfg) =>
                         {
                             cfg.Host("localhost", "/", h => {
                                 h.Username("guest");
                                 h.Password("guest");
                             });
                             
                             // When configuring endpoints manually, ConfigureEndpoints should be excluded or be called after any explicitly configured receive endpoints.
                             cfg.ConfigureEndpoints(context);
                         });
                     });
                     
                     services.AddHostedService<Project2Worker>();
                 })
                 .Build();

await host.RunAsync();