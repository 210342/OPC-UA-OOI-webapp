# M2M Communication - Services

The goal of this project is to provide an additional layer of abstraction, so that ASP.NET DI container doesn't force the hard dependencies on the OPC-UA library. Most of them are simply wrappers for interfaces from [M2MCommunication.Core](../M2MCommunication.Core/README.md) which they inject using `UaooiServiceLocator`. For the services to work it is neccessary to initialise them - `ServiceContainerSetup` class provides that functionality.

## Services

| Name | Goal |
|:----:|:-----|
| `ConfigurationService` | A wrapper for an injected `IConfiguration` implementation |
| `MessageBusService` | A wrapper for an injected `IMessageBus` implementation |
| `SubscriptionFactoryService` | A wrapper for an injected `ISubscriptionFactory` implementation |

## Helper classes

| Name | Goal |
|:----:|:-----|
| `UaooiServiceLocator` | An implementation of `IServiceLocator` which extends `CommonServiceLocator.ServiceLocatorImplBase`. Also implements `IDisposable` |
| `ServiceContainerSetup` | A class which should be used to initialise a DI container of the `ServiceLocator` |
