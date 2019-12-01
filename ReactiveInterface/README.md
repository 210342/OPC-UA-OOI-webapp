# Reactive interface

Module that targets parsing of the receiving data and preparing it for display. It processes any data bindings provided by the injected Message Bus.

## Projects

- [ReactiveInterface](ReactiveInterface)
  - Contains core functionality of the parsing mechanism
- [InterfaceModel](InterfaceModel)
  - Contains the model used in the ReactiveInterface project and provides means to persist it
- ReactiveInterfaceUnitTest
  - Contains unit tests for every project in this module
  - Implemented using [xUnit](https://xunit.net) framework
