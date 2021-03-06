# M2M Communication - Services

The goal of this project is to provide an additional layer of abstraction, so that ASP.NET DI container doesn't force the hard dependencies on the OPC-UA library. Most of them are simply wrappers for interfaces from [M2MCommunication.Core](../M2MCommunication.Core/README.md) which they inject using `UaooiServiceLocator`. For the services to work it is neccessary to initialise them - `ServiceContainerSetup` class provides that functionality.

## Services

| Name | Description |
|:----:|:-----|
| `MessageBusService` | A wrapper for an injected `IMessageBus` implementation |

### *MessageBusService*

> Implements `IDisposable`

A wrapper for an injected `IMessageBus` implementation

#### Constructors

| Name | Description |
|:----:|:------------|
| MessageBusService() | Default constructor, tries to inject an instance of `IMessageBus` implementation into its property |

#### Properties

| Type | Name | Accessors | Description |
|:----:|:----:|:---------:|:------------|
| `IMessageBus` |**MessageBus**| get; | Injected `IMessageBus` service |

#### Methods

| Return type | Name |  Description |
|:----:|:----:|:------------|
| `void` | Dispose() | Disposes the `MessageBus` property |

## MEF helper classes

| Name | Description |
|:----:|:-----|
| `UaooiServiceLocator` | An implementation of `IServiceLocator` which extends `CommonServiceLocator.ServiceLocatorImplBase`. Also implements `IDisposable` |
| `ServiceContainerSetup` | A class which should be used to initialise a DI container of the `ServiceLocator` |

### *UaooiServiceLocator*

> Extends `ServiceLocatorImplBase`

> Implements `IDisposable`

An implementation of a service locator design pattern.

#### Constructors

| Name | Description |
|:----:|:------------|
| UaooiServiceLocator(`CompoistionContainer` compositionContainer) | Sets up the object. Throws `ArgumentNullException` if the compositionContainer is null |

#### Methods

| Return type | Name |  Description |
|:----:|:----:|:------------|
| `void` | Dispose() | Disposes the composition container |
| `IEnumerable<object>` | DoGetAllInstances(`Type` serviceType) | Gets all instances of the `serviceType` (overriden from `ServiceLocatorImplBase`) |
| `object` | DoGetInstance(`Type` serviceType, `string` key) | Gets an instance of the `serviceType` with the given `key` (overriden from `ServiceLocatorImplBase`) |

### *ServiceContainerSetup*

> Implements `IDisposable`

A class used to initialise a container for the `UaooiServiceLocator` to use. Requires `UaLibrary` section from application settings.

#### Constructors

| Name | Description |
|:----:|:------------|
| ServiceContainerSetup(`UaLibrarySettings` settings) | Sets up the object with passed in configuration |

#### Methods

| Return type | Name |  Description |
|:----:|:----:|:------------|
| `void` | Dispose() | Disposes private members |
| `ServiceContainerSetup` | Initialise() | Initialises a composition container and sets up a service locator for OPC-UA library |
