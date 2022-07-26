# Injection ![](https://github.com/Alfred-Sung/Injection/actions/workflows/dotnet.yml/badge.svg)

Simple DI library without the need for containers.

### Example
```c#
using Injection;
using Injection.Attribute;

[Injectable(typeof(Service))] 
public interface IService  {
    //...
}

public class Service : IService {
    //...
}
```

```c#
using Injection;

public class Client {
    [Inject] IService service;
    //...
}

static void Main(string[] args) {
    Client client = Injector.Get<Client>();
    //...
}
```



## Injector Class
Namespace: Injection

Exposes all the functionality of the DI library.

```c#
public sealed class Injector
```
Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object?view=net-5.0) → Injector

### Static Methods
| Signature | Description |
|--|--|
| T Get\<T>()  | Instantiate and return object of type T with injected services. |
| object Get(Type)  | Instantiate and return object with injected services. |
| void InjectExisting(object)  | Inject services into an existing object. Only injects to its fields and properties. |

### Injector.Get\<T>()
Create and return an object of type T with injected services. 

#### Type Parameters
`T`

The type of the object to create.

#### Returns
`T`

The instantiated object.

#### Exceptions

`AttributeException`

Thrown if `[Injectable]` does not decorate either an interface or an abstract class.

`AttributeException`

Thrown if there are multiple constructors with `[Default]` attributes.

`NoImplementationException`

Thrown if `[Injectable]` targetInstance does not implement T.

`CircularDependencyException`

Thrown if a dependency is found referencing an earlier service.

`MissingMethodException`

Thrown if DI library cannot find a suitable constructor for T.


### Injector.Get(Type type)
Create and return an object of type with injected services. 

#### Parameters
`Type type`

The type of the object to create.

#### Returns
`object`

The instantiated object.

#### Exceptions

`AttributeException`

Thrown if `[Injectable]` does not decorate either an interface or an abstract class.

`AttributeException`

Thrown if there are multiple constructors with `[Default]` attributes.

`NoImplementationException`

Thrown if `[Injectable]` targetInstance does not implement type.

`CircularDependencyException`

Thrown if a dependency is found referencing an earlier service.

`MissingMethodException`

Thrown if DI library cannot find a suitable constructor for type.


### Injector.InjectExisting(object obj)
Inject services into an existing object. Only injects to its fields and properties.

#### Parameters
`object obj`

The object to inject fields and properties.

#### Exceptions

`AttributeException`

Thrown if `[Injectable]` does not decorate either an interface or an abstract class.

`AttributeException`

Thrown if there are multiple constructors with `[Default]` attributes.

`NoImplementationException`

Thrown if `[Injectable]` targetInstance does not implement type.

`CircularDependencyException`

Thrown if a dependency is found referencing an earlier service.

`MissingMethodException`

Thrown if DI library cannot find a suitable constructor for type.

___

## \[Injectable] attribute
Namespace: Injection.Attribute

```c#
[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class InjectableAttribute : Attribute
```
Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object?view=net-5.0) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute?view=net-5.0) → InjectableAttribute 

Attributes [AttributeUsageAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute?view=net-5.0 "System.AttributeUsageAttribute")

### Constructor
InjectableAttribute(Type targetInstance, Lifetime lifetime = Lifetime.Transient)

### Parameters
| Parameter | Description |
|--|--|
| `targetInstance` | Chosen implementation of the interface to use that will be injected to clients. |
| `lifetime` | Specifies when a service is created for each client. Default transient. |


### Remarks
- `[Injectable]` can only be applied to an `interface` or abstract classes.
- Requiring to specify `targetInstance` solves the ambiguity of which implementation to use in the case of multiple implementations.
- There are three types of lifetimes:

| Lifetimes | Description |
|--|--|
| Scoped | Same instance for each client but different between clients. |
| Transient | Different instances for every request. |
| Singleton | Single instance shared across all clients. |
___

## \[Inject] attribute
Namespace: Injection.Attribute

```c#
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class InjectAttribute : Attribute
```
Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object?view=net-5.0) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute?view=net-5.0) → InjectAttribute 

Attributes [AttributeUsageAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute?view=net-5.0 "System.AttributeUsageAttribute")

### Constructor
InjectAttribute()

### Remarks
- `[Inject]` can only be applied to fields or properties.
- Decorated fields and properties will be injected regardless of access modifiers
- Static fields and properties can also be injected

___

## \[Default] attribute
Namespace: Injection.Attribute

```c#
[AttributeUsage(AttributeTargets.Constructor)]
public class DefaultAttribute : Attribute
```
Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object?view=net-5.0) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute?view=net-5.0) → DefaultAttribute 

Attributes [AttributeUsageAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute?view=net-5.0 "System.AttributeUsageAttribute")

### Constructor
DefaultAttribute()


### Remarks
- The `[Default]` attribute is used to mark which constructor to use when assembling an object.
- Decorating a constructor is necessary if there are multiple declared constructors.
- If there are no declared constructors or multiple undecorated constructors, the DI library will use the empty parameter constructor.
