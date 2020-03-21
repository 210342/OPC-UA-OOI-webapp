# Reactive interface

Package that targets modeling of the receiving data and preparing it for display. It uses aliases of the UA types provided by `ReactiveHMI.M2MCommunication.Core.Interfaces.IConfiguration` to determine how to display a model.

## Projects

- [TemplateRepositories](TemplateRepositories)
  - Contains the model used in the ReactiveInterface module in the ReferenceWebApplication project and provides means to persist it
- ReactiveInterfaceUnitTest
  - Contains unit tests for every project in this module
  - Implemented using [xUnit](https://xunit.net) framework
