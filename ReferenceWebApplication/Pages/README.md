# Pages

Blazor lets developers combine the razor components with C# code for handling events and data bindings. It's not neccessary to use JavaScript, although embedding JS, as well as [JS interop](https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interop?view=aspnetcore-3.0) is possible.

## Directives

A directive is a keyword preceded by a `@`. Blazor defines multiple directives for a variety of purposes:

| Directive   |      Purpose      |
|----------|:-------------|
| **page** |  defines the address of the page relative to the domain name |
| **using** |    imports a namespace   |
| **implements** | signals to the compiler that this rendered component or page should implement the following interface |
| **inherits** |  signals to the compiler that this rendered component or page should inherit from the following type |
| **inject** | creates a property of the specified type and name which will be then set with DI pattern if the specified type has been registered as a service in the DI container |
| **code** | defines a block of C# code which contains overriden lifecycle methods, event handlers or any data needed to be displayed |
