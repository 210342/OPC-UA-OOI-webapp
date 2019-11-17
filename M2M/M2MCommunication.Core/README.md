# M2M Communication - Core

The goal of this project is to provide interfaces used in the whole application as weel as some common types.

## Interfaces

| Name | Goal |
|:----:|:-----|
| `ISubscription` | Interface for a subscription to implement. Contains neccessary properties and methods |
| `IConfiguration` | Interface for an implementation of configuration for either a consumer or producer |
| `ISubscriptionFactory` | Interface for an object which is to be used for subscribing to the data types |
| `IMessageBus` | Interface for an object representing a bus that will notify subscribers about changes in the data they subscribed to |

### *ISubscription*

An interface for a subscription to implement.

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
|`UaTypeMetadata`|**UaTypeMetadata**| get; | Metadata of the type subscribed |
|`object`|**Value**| get; set; | Current value of this subscription |

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `void` | Enable(`PropertyChangedEventHandler` handler) | Used to associate an event handler with this subscription |
| `void` | Disable() | Used to remove all event handlers from the subscription |

### *ISubscriptionFactory*

An interface for an implementation of configuration for either a consumer or producer

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `ISubscription` | Subscribe(`UaTypeMetadata` uaTypeMetadata, `PropertyChangedEventHandler` handler) | Used to return a subscription for the specified type and with the specified event handler |

### *IConfiguration*

An interface for an implementation of configuration for either a consumer or producer

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `IEnumerable<UaTypeMetadata>` | GetTypeMetadata() | Used to retrieve metadata of all of the configured data types |

### *IMessageBus*

An interface for an object representing a bus that will notify subscribers about changes in the data they subscribed to

#### Methods

| Return type | Name |  Description |
|:-----------:|:----:|:-------------|
| `void` | Initialise(`UaLibrarySettings` settings)| Initialises the message bus with the specified settings |
| `Task` | InitialiseAsync(`UaLibrarySettings` settings)| Initialises the message bus with the specified settings asynchronously |

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
|`string`|ConsumerConfigurationFile| get; set; | Name of the file containing a configuration of the consumer |
|`string`|ResourcesDirectory| get; set; | Path to the direcotry of the application's resources |
|`string`|LibraryDirectory| get; set; | Path to the direcotry of the OPC-UA library relative to the resources directory |

### *UaTypeMetadata*

A POCO object representing the type of a UA object

#### Constructors

| Name | Description |
|:----:|:------------|
|UaTypeMetadata(`string` repositoryGroupName, `string` typeName)| Sets corresponding properties; throws `ArgumentNullException` when the `typeName` is either null, empty or contains only whitespace |

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
|`string`|TypeName| get; | Name of the type; required |
|`string`|RepositoryGroupName| get; | Name of the repository the type belongs to |

## Exceptions

This project also provides definitions of application-scoped exceptions.

| Name | Base type | Possible cause |
|:----:|:---------:|:---------------|
| `ComponentNotIntialisedException` | `Exception` | Thrown when something tries to call an uninitialised object or an object on which DI failed to inject a property |
| `ConfigurationFileNotFoundException` | `FileNotFoundException` | Thrown when the application couldn't find the configuration file (Either be it a OPC-UA consumer configuration or any other) |
| `UnsupportedTypeException` | `Exception` | Thrown when the configuration implies a subscription to a type which isn't supported by the library or when trying to subscribe to a type that is not contained within the configuration |
| `ValueRankOutOfRangeException` | `ArgumentOutOfRangeException` | Thrown when the configuration implies a subscription to a type with an unsupported value of the `ValueRank` property (for example multidimensional arrays - values >= 2) |
