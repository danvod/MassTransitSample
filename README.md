Example solution with some MassTransit examples.





## Projects

### Messaging

Holds message contracts together with custom Publish and Consume filters.



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

### Alert

A HostedService project consuming the `AlertEvent`. Minimal, automatic configuration. `AlertConsumer` consumes `AlertEvent` which is published from both Project1 and Project2.



Shows

- multiple producers with 1 consumer
- 2 different ways of publishing
- passing custom headers

### Project1 & Project2

Both consume `NotificationEvent` and `RoundRobinEvent`



Project1 has ReceiveEndpoint configured in the startup.

Project2 shows an alternative way of configuring a receive endpoint with `NotificationConsumerDefinition`



When ConsoleProducer is ran with `dotnet run -- NotificationEvent`, 

- PubSub example - a message `NotificationEvent` handled by `NotificationConsumer` in Project1 & Project2
- Publish & Consume filters - Project1 has Publish & Consume filters configured, meaning we can pass arbitrary values in the headers. The way it's configured, it will apply to all producers and consumers.



When ConsoleProducer is ran with `dotnet run -- RoundRobinEvent`

- multiple instances (or different instances), consuming the same message RoundRobinEvent in a round-robin fashion. Unlike in `NotificationEvent`, dedicated queues are not set up for each service.

### SendConsumer

Consums `DirectEvent`

### 