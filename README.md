Example solution with some [MassTransit](https://masstransit.io/) examples for RabbitMQ



## Pre-Requisites

```powershell
docker run --hostname localhost --rm -d -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management
```



To run all consumers

```
./run_consumers.sh
```



## Projects

### Messaging

Holds message contracts together with custom Publish and Consume filters.



Additionally, it shows very basic implementations of `IFilter<ConsumeContext<T>>` and `IFilter<PublishContext<T>>`  which are used by Project1.

### ConsoleProducer

The primary Message producer. A different producer will publish/send messages depending on the command line arguments.



Options:

- NotificationEvent
- RoundRobinEvent
- DirectA
- DirectB



```
dotnet run -- {option}
```

------

### Alert

A HostedService project consuming the `AlertEvent`. Minimal, automatic configuration. `AlertConsumer` consumes `AlertEvent,` which is published from both Project1Worker and Project2Worker.



Shows

- multiple producers with 1 consumer

- 2 different ways of publishing - `bus.Publish<T>` vs `bus.Publish(new ())`

- passing custom headers

  

------

### Project1 & Project2

Both consume `NotificationEvent` and `RoundRobinEvent`



Project1 has a ReceiveEndpoint configured in the startup.

Project2 shows an alternative way of configuring a receive endpoint with a  `NotificationConsumerDefinition`



Run ConsoleProducer with `dotnet run -- NotificationEvent` to see

- PubSub example - a message `NotificationEvent` handled by `NotificationConsumer` in Project1 & Project2
- Publish & Consume filters - Project1 has Publish & Consume filters configured, meaning we can pass arbitrary values in the headers. The way it's configured, it will apply to all producers and consumers.



Run ConsoleProducer with `dotnet run -- RoundRobinEvent` to see

- multiple instances (or different instances), consuming the same message (`RoundRobinEvent`) in a round-robin fashion. Unlike in `NotificationEvent`, dedicated queues are not set up for each service.

------

### DirectConsumer

Consumes `DirectEvent`. This project shows

- direct exchange configuration
- examples of `Send` or `Publish` for a direct message

### 