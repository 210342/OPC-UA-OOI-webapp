# M2M Communication - UAOOI

The goal of this project is to implement the [UAOOI](https://https://github.com/mpostol/OPC-UA-OOI) library for this application

## Types

| Name | Description |
|:----:|:------------|
| `Subscription` | An implementation of the `ISubscription` interface used in `ISubscriptionFactory` implementation in this project |

### *Subscription*

> Implements `ISubscription`

An implementation of the `ISubscription` interface used in `ISubscriptionFactory` implementation in this project.

#### Constructors

| Name | Description |
|:----:|:------------|
| Subscription(`UATypeInfo` typeInfo, `UaTypeMetadata` uaTypeMetadata, `string` alias, `object` value) | Constructs the object and initialises it with provided values |

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `UaTypeMetadata` | UaTypeMetadata | get; | Metadata of the type subscribed |
| `string` | TypeAlias | get; | Alias used by the reactive interface to understand how to represent the type |
| `UATypeInfo` | TypeInfo | get; | Description of the type provided from UAOOI library |
| `object` | Value | get; set; | The subscribed object |

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `void` | Enable(`PropertyChangedEventHandler` handler) | Removes all existing event handlers and then adds the provided handler |
| `void` | Disable() | Removes all existing event handlers |
| `void` | InvokeValueUpdated() | Used to invoke an internal ValueUpdated event manually |

## Injections

| Name | Description |
|:----:|:------------|
| `Configuration` | Represents the configuration of the OPC-UA consumer; extends `ConfigurationFactoryBase` and implements `IConfiguration` |
| `ConsumerBindingFactory` | Implementation of `IBindingFactory` and `ISubscriptionFactory` |
| `UaooiMessageBus` | Represents the message bus; extends `DataManagementSetup` and implements `IMessageBus` |

### *Configuration*

> Extends `ConfigurationFactoryBase`
>
> Implements `IConfiguration`

A representation of the consumer's configuration.

#### Constructors

| Name | Description |
|:----:|:------------|
| Configuration(`ILogger` logger, `string` configurationFileName) | Initialises the object with provided logger and a name of the configuration file |

#### Events

| Type | Name | Description |
|:----:|:----:|:------------|
| `EventHandler<EventArgs>` | OnAssociationConfigurationChange | Occurs after the association configuration has been changed |
| `EventHandler<EventArgs>` | OnMessageHandlerConfigurationChange | Occurs after the communication configuration has been changed |

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `string` | GetAliasForRepositoryGroup(`string` repositoryGroupName) | Used to read an alias connected to the repository's type |

### *ConsumerBindingFactory*

> Implements `IBindingFactory`, `ISubscriptionFactory`

An implementation of `IBindingFactory` and `ISubscriptionFactory` which provides both data bindings as well as subscriptions.

#### Constructors

| Name | Description |
|:----:|:------------|
| ConsumerBindingFactory(`ILogger` logger, `IConfiguration` configuration) | Initialises the object with provided logger and configuration instances |

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `IConsumerBinding` | GetConsumerBinding(`string` repositoryGroup, `string` processValueName, `UATypeInfo` fieldTypeInfo) | Gets a consumer binding for a type specified by the parameters |
| `IProducerBinding` | GetProducerBinding(`string` repositoryGroup, `string` processValueName, `UATypeInfo` fieldTypeInfo) | Throws `NotSupportedException` since the application works only in the consumer mode |
| `void` | Initialise(`IConsumerViewModel` consumerViewModel) | Derived from `IConfiguration`; injects `IConsumerViewModel` into the instance |

### *UaooiMessageBus*

> Extends `DataManagementSetup`
>
> Implements `IMessageBus`, `IDisposable`

Represents a message bus which notifies subscribers about updates in the data they subscribed to.

#### Constructors

| Name | Description |
|:----:|:------------|
| UaooiMessageBus(`IConfiguration` configuration, `IEncodingFactory` encodingFactory, `ISubscriptionFactory` subscriptionFactory, `IMessageHandlerFactory` messageHandlerFactory, `ILogger` logger) | Initialises the object with provided parameters |

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `IBindingFactory` | BindingFactory | get; set; | Inherited from `DataManagementSetup`. An instance of the `IBindingFactory` service used to bind data |
| `IEncodingFactory` | EncodingFactory | get; set; | Inherited from `DataManagementSetup`. Provides functionality to lookup a dictionary containing value converters |
| `IMessageHandlerFactory` | MessageHandlerFactory | get; set; | Inherited from `DataManagementSetup`. Creates objects supporting the Data Transfer Graph messages handling over the wire |
| `IConfigurationFactory` | ConfigurationFactory | get; set; | Inherited from `DataManagementSetup`. Parses and handles configuration |

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `void` | Initialise(`IConsumerViewModel` consumerViewModel) | Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data |
| `Task` | InitialiseAsync(`IConsumerViewModel` consumerViewModel) | Starts this instance asynchronously - Initializes the data set infrastructure, enables all associations and starts pumping the data |
| `void` | RefreshConfiguration() | Reads the configuration again and restarts the process of reading data |

## Extensions

| Name | Description |
|:----:|:------------|
| `ConfigurationExtension` | A type that extends `ConfigurationData` and provides mappings of types used in the configuration to aliases used to create interfaces for the types |
| `InformationModelAlias` | A serializable POCO object representing mappings of the UA types to aliases recognised by the reactive interface |
| `UATypeInfoExtensions` | A set of extension methods for `UATypeInfo` type |

### UATypeInfoExtensions

A set of extension methods for the `UATypeInfo` type

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `bool` | ContainsArray(`this UATypeInfo` typeInfo) | Checks whether the specified `typeInfo` definitely describes an array |
| `bool` | ContainsMultidimensionalArray(`this UATypeInfo` typeInfo) | Checks whether the specified `typeInfo` definitely describes an array of multiple dimensions |

### ConfigurationExtension

> Extends `ConfigurationData`

An extension of the `ConfigurationData` type. Adds type-alias mapping to the consumer configuration

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `IEnumerable<InformationModelAlias>` | Aliases | get; | Array of UA type aliases |

### InformationModelAlias

A POCO object representing a type-alias mapping

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `Uri` | InformationModelUri | get; set; | URI of the UA type |
| `string` | Alias | get; set; | Alias of the type used by the reactive interface |
