# M2M Communication - Core

The goal of this project is to provide interfaces used in the whole application as well as some common types.

## Interfaces

| Name | Goal |
|:----:|:-----|
| `IConfiguration` | Interface for an implementation of configuration for either a consumer or producer |
| `IConsumerViewModel` | Interface for an implementation of a view model which should handle any new subscriptions |
| `ILogger` | Interface for a logger to implement |
| `ILoggerContainer` | An interface for a type that aggregates all external loggers and merges them into a single sink |
| `IMessageBus` | Interface for an object representing a bus that will notify subscribers about changes in the data they subscribed to |
| `ISubscription` | Interface for a subscription to implement. Contains neccessary properties and methods |
| `ISubscriptionFactory` | Interface for an object which is to be used for subscribing to the data types |

### *IConfiguration*

An interface for an implementation of configuration for either a consumer or producer
*Implemented by* ReactiveHMI.M2MCommunication.UaooiInjections.Components.Configuration

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `string` | GetAliasForRepositoryGroup(`string` repositoryGroupName) | Provides an alias for a given repositiory group which can be used by other packages to identify the group in case URI format is not preferable |

### *IConsumerViewModel*

Interface for an implementation of a view model which should handle any new subscriptions
*Implemented by* ReactiveHMI.ReferenceWebApplication.ReactiveInterface.MessageParser

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `void` | AddSubscription(`ISubscription` subscription) | Handles a new subscription |

### *IMessageBus*

An interface for an object representing a bus that will notify subscribers about changes in the data they subscribed to
*Implemented by* ReactiveHMI.M2MCommunication.UaooiInjections.Components.UaooiMessageBus

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `void` | Initialise(`IConsumerViewModel` consumerViewModel)| Injects specified view model and starts communication |
| `Task` | InitialiseAsync(`IConsumerViewModel` consumerViewModel)| Injects specified view model and starts communication asynchronously |
| `void` | RefreshConfiguration() | Reads the configuration again and restarts the process of processing data |

### *ILogger*

Interface for a logger to implement
*Implemented by* ReactiveHMI.ReferenceWebApplication.Services.Logger

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `void` | LogInfo(`string` message, `string` callerName, `string` callerPath)| Logs specified message with `information` level |
| `void` | LogWarning(`string` message, `string` callerName, `string` callerPath) | Logs specified message with `warning` level |
| `void` | LogWarning(`Exception` exception, `string` message, `string` callerName, `string` callerPath) | Logs specified message and exception message with `warning` level |
| `void` | LogError(`string` message, `string` callerName, `string` callerPath) | Logs specified message with `error` level |
| `void` | LogError(`Exception` exception, `string` message, `string` callerName, `string` callerPath) | Logs specified message and exception message with `error` level |

### *ILoggerContainer*

An interface for a type that aggregates all external loggers and merges them into a single sink
*Implemented by* ReactiveHMI.M2MCommunication.UaooiInjections.LoggerContainer

#### *Methods*

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `ILoggerContainer` | EnableLoggers() | Aggregates all injected loggers and merges them into a single sink and returns this instance |

### *ISubscription*

An interface for a subscription to implement.
*Implemented by* ReactiveHMI.M2MCommunication.UaooiInjections.Subscription

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `UaTypeMetadata` | UaTypeMetadata | get; | Metadata of the type subscribed |
| `string` | TypeAlias | get; | Alias used by the reactive interface to understand how to represent the type |
| `object` | Value | get; set; | Current value of this subscription |

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `void` | Enable(`PropertyChangedEventHandler` handler) | Used to associate an event handler with this subscription |
| `void` | Disable() | Used to remove all event handlers from the subscription |
| `void` | InvokeValueUpdated() | Used to invoke an internal ValueUpdated event manually |

### *ISubscriptionFactory*

An interface for an implementation of configuration for either a consumer or producer
*Implemented by* ReactiveHMI.M2MCommunication.UaooiInjections.Components.ConsumerBindingFactory

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `void` | Initialise(`IConsumerViewModel` consumerViewModel) | Initialises the instance by providing the view model of the consumner's interface |

## Common types

| Name | Description |
|:----:|:-----|
| `UaLibrarySettings` | A POCO object representing the configuration of the adapted OPC-UA library |
| `UaTypeMetadata` | A POCO object representing the type of a UA object |

### *UaLibrarySettings*

A POCO object representing the configuration of the adapted OPC-UA library

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `string` | ConsumerConfigurationFile | get; set; | Name of the file containing a configuration of the consumer |
| `string` | ResourcesDirectory | get; set; | Path to the direcotry of the application's resources |
| `string` | LibraryDirectory | get; set; | Path to the direcotry of the OPC-UA library relative to the resources directory |

### *UaTypeMetadata*

A POCO object representing metadata the type of a UA object

#### Constructors

| Name | Description |
|:----:|:------------|
|UaTypeMetadata(`string` repositoryGroupName, `string` typeName)| Sets corresponding properties; throws `ArgumentNullException` when the `typeName` is either null, empty or contains only whitespace |

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
|`string`| TypeName | get; | Name of the type; required |
|`string`| RepositoryGroupName | get; | Name of the repository the type belongs to |

### *UaContractNames*

A class containing const string values for MEF container to use as contract names

#### Fields

| Type | Name | Description |
|:----:|:----:|:------------|
|`string`| ConfigurationFileNameContract | Contract name for a configuration filename |

## Exceptions

This project also provides definitions of application-scoped exceptions.

| Name | Base type | Possible cause |
|:----:|:---------:|:---------------|
| `ComponentNotIntialisedException` | `Exception` | Thrown when something tries to call an uninitialised object or an object on which DI failed to inject a property |
| `ConfigurationFileNotFoundException` | `FileNotFoundException` | Thrown when the application couldn't find the configuration file (Either be it a OPC-UA consumer configuration or any other) |
| `UnsupportedTypeException` | `Exception` | Thrown when the configuration implies a subscription to a type which isn't supported by the library or when trying to subscribe to a type that is not contained within the configuration |
| `ValueRankOutOfRangeException` | `ArgumentOutOfRangeException` | Thrown when the configuration implies a subscription to a type with an unsupported value of the `ValueRank` property (for example multidimensional arrays - values >= 2) |
