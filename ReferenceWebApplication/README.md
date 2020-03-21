# ReferenceWebApplication

This package focuses on pure **ASP**.**NET** elements, which are:

- Server configuration
  - [App settings](appsettings.json)
- Logging
  - [Program netry point](Program.cs)
  - [Logger (as a service)](Services/Logger.cs)
- Dependency injection container and services
  - [Startup](Startup.cs)
- Graphical interface (using [Server Side Blazor](https://blazor.net))
  - [ReactiveInterface](ReactiveInterface) - Core logic behind reactive interface based on UA types
  - [Namespaces](_Imports.razor)
  - [Imports](Pages/_Host.cshtml)
  - [Components](Components)
  - [Global components](Shared)
  - [Pages](Pages)
  - [Static resources](wwwroot)
