# Machine to machine communication

Following set of modules targets the functionality of communication between IoT machines using [OPC-UA](https://opcfoundation.org/about/opc-technologies/opc-ua/) standard, specifically [Part 14: PubSub](https://opcfoundation.org/developer-tools/specifications-unified-architecture/part-14-pubsub/).

## Projects

- [M2MCommunication.Core](M2MCommunication.Core/README.md)
  - Contains core interfaces and common types referenced within the whole application
- [M2MCommunication.Services](M2MCommunication.Services/README.md)
  - Provides an extra layer of adapters allowing native **ASP**.**NET** DI container to register and inject necessary OPC-UA services without any hard dependencies
- [M2MCommunication.Uaooi](M2MCommunication.Uaooi/README.md)
  - Contains extensions of the [UAOOI](https://github.com/mpostol/OPC-UA-OOI) package with adapters used for dependency injection
- M2MCommunication.UnitTest
  - Contains unit tests for every project in this directory
  - Implemented using [xUnit](https://xunit.net) framework
