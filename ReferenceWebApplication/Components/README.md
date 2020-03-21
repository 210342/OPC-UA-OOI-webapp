# Components

Components can be defined in a similar manner to the pages, they simply don't use the `@page` directive. Any other directives are valid in this context

## Directives

| Directive   |      Purpose      |
|-------------|----------------|
| **using** |    imports a namespace   |
| **implements** | signals to the compiler that this rendered component should implement the following interface |
| **inherits** |  signals to the compiler that this rendered component should inherit from the following type |
| **inject** | creates a property of the specified type and name which will be then set with DI pattern if the specified type has been registered as a service in the DI container |
| **code** | defines a block of C# code which contains overriden lifecycle methods, event handlers or any data needed to be displayed |

## Parameters

Usually components receive parameters from their parent components. To define a parameter there needs to be a public property in the `@code` block with a [ParameterAttribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.parameterattribute?view=aspnetcore-3.0). The name of the property will be the name of the parameter. Then the parent can send a parameter with the following syntax:

``` html
<ChildComponent ParameterName=@localVariable />
```

## Defined components

### ImageMessageParser

Component used to render each ImageTemplate from an injected `ReactiveHMI.ReferenceWebApplication.ReactiveInterface.ImageMessageParser` as an image with the properties `Property` drawn over them.

### TextMessageParser

Component used to print to the screen any properties that don't have any graphical representation defined in the ImageTemplate 

### Property

Component used to render a single property that updates each time the corresponding subscription is updated
