# M2M Communication - UAOOI

The goal of this project is to implement the [UAOOI](https://https://github.com/mpostol/OPC-UA-OOI) library for this application

## Types

| Name | Description |
|:----:|:------------|
| `Subscription` | An implementation of the `ISubscription` interface used in `ISubscriptionFactory` implementation in this project |
| `Configuration` | Represents the configuration of the OPC-UA consumer; extends `ConfigurationFactoryBase` and implements `IConfiguration` |
| `ConsumerBindingFactory` | Implementation of `IBindingFactory` and `ISubscriptionFactory` |
| `UaooiMessageBus` | Represents the message bus; extends `DataManagementSetup` and implements `IMessageBus` |
| `UATypeInfoExtensions` | A set of extension methods for `UATypeInfo` type |

### *Subscription*

> Implements `ISubscription`

An implementation of the `ISubscription` interface used in `ISubscriptionFactory` implementation in this project.

#### Constructors

| Name | Description |
|:----:|:------------|
| Subscription(`UATypeInfo` typeInfo, `string` typeName, `object` value) | Constructs the object and initialises it with provided values |

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `string` | TypeName | get; | Name of the subscribed type |
| `UATypeInfo` | TypeInfo | get; | Description of the type provided from UAOOI library |
| `object` | Value | get; set; | The subscribed object |

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `void` | Enable(`PropertyChangedEventHandler` handler) | Removes all existing event handlers and then adds the provided handler |
| `void` | Disable() | Removes all existing event handlers |

### *Configuration*

> Extends `ConfigurationFactoryBase`
>
> Implements `IConfiguration`

A representation of the consumer's configuration.

#### Constructors

| Name | Description |
|:----:|:------------|
| Configuration() | Default constructor; Initialises the object |

#### Events

| Type | Name | Description |
|:----:|:----:|:------------|
| `EventHandler<EventArgs>` | OnAssociationConfigurationChange | Occurs after the association configuration has been changed |
| `EventHandler<EventArgs>` | OnMessageHandlerConfigurationChange | Occurs after the communication configuration has been changed |

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `void` | Initialise(`string` configurationFileName) | Initialises neccessary values that are not provided through dependency injection |
| `IEnumerable<string>` | GetDataTypeNames() | Returns all data names specified in the configuration |

### *ConsumerBindingFactory*

> Implements `IBindingFactory`, `ISubscriptionFactory`

An implementation of `IBindingFactory` and `ISubscriptionFactory` which provides both data bindings as well as subscriptions.

#### Constructors

| Name | Description |
|:----:|:------------|
| Configuration() | Default constructor; Initialises the object |

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `IConsumerBinding` | GetConsumerBinding(`string` repositoryGroup, `string` processValueName, `UATypeInfo` fieldTypeInfo) | Gets a consumer binding for a type specified by the parameters |
| `IProducerBinding` | GetProducerBinding(`string` repositoryGroup, `string` processValueName, `UATypeInfo` fieldTypeInfo) | Throws `NotSupportedException` since the application works only in the consumer mode |
| `ISubscription` | Subscribe(`string` subscriptionName, `PropertyChangedEventHandler` handler) | Gets a subscription for a type specified by the `subscriptionName`. Can throw an `UnsupportedTypeException` |

### *UaooiMessageBus*

> Extends `DataManagementSetup`
>
> Implements `IMessageBus`, `IDisposable`

Represents a message bus which notifies subscribers about updates in the data they subscribed to.

#### Constructors

| Name | Description |
|:----:|:------------|
| UaooiMessageBus(`IConfiguration` configuration, `IEncodingFactory` encodingFactory, `ISubscriptionFactory` subscriptionFactory, `IMessageHandlerFactory` messageHandlerFactory) | Initialises the object with provided parameters, which should be injected |

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
| `void` | Initialise(`UaLibrarySettings` settings) | Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data |

### UATypeInfoExtensions

A set of extension methods for the `UATypeInfo` type

#### Methods

| Return type | Name | Description |
|:-----------:|:----:|:------------|
| `bool` | ContainsArray(`this UATypeInfo` typeInfo) | Checks whether the specified `typeInfo` definitely describes an array |
| `bool` | ContainsMultidimensionalArray(`this UATypeInfo` typeInfo) | Checks whether the specified `typeInfo` definitely contains an array of multiple dimensions |
