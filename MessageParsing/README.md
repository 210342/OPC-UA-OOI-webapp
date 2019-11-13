# Message Parsing

Module that targets parsing of the receiving data and preparing it for display. It reads the consumer configuration, subscribes to the specified types and processes them.

## Projects

- [MessageParsing](MessageParsing/README.md)
  - Contains core functionality of the parsing mechanism
- [InterfaceModel](InterfaceModel/README.md)
  - Contains the model used in the MessageParsing project and provides means to persist it
- MessageParsingUnitTest
  - Contains unit tests for every project in this directory
  - Implemented using [xUnit](https://xunit.net) framework
