![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Results

This package provides a **lightweight, functional-style result abstraction** for explicit error handling in .NET.

It introduces **`Result`** and **`Result<TValue>`** types, inspired by functional programming 
and discriminated unions, while remaining idiomatic to the .NET ecosystem.

A `Result` represents an outcome of an operation, which can either be a `Success`, or a `Failure`.
The success path can contain a value, represented by `Result<TValue>`, where `TValue` is the inner value.
The failure path contains failure information, modelled as an `Failure` record.

Using results enhances code clarity and reliability by enabling clear, exception-free handling of success, failure, and validation outcomes.

## Features

- **Explicit error handling:** Avoid exceptions for control flow; return meaningful failures instead.
- **Fluent extension methods:** Compose operations with `Bind`, `Map`, `Match`, `Tap`, and more.
- **Strongly typed generic results:** Preserve either a success value or failure details in one type-safe container.
- **Rich validation support:** Aggregate validation failures with rich property-level details.
- **Seamless async support:** Works naturally with Task and async pipelines.
- **Functional programming inspired:** Inspired by FP principles for predictable, readable, and maintainable error handling.

---

## Contents
1. [Quick start](#quick-start)
1. [Core concepts](#core-concepts)
1. [Extensions](#extensions)
1. [Collections](#collections)
1. [LINQ Query syntax support](#linq-query-syntax-support)
1. [Test extensions](#test-extensions)
1. [Packages](#packages)
1. [Inspiration](#inspiration)
1. [Conclusion](#conclusion)

---

## Quick start

This example demonstrates the most common workflow when using results:
construction, transformation and consumption.

```csharp
using Toarnbeike.Results;			        // Base namespace for Result, Result<TValue> and Failure
using Toarnbeike.Results.Extensions;		// For functional extensions on Result and Result<TValue>
```

A result can represent either success (with or without a value) or failure (with error details):

```csharp
// Success Results
Result<int> result = 42; 
Result<int> failureResult = new Failure("Code", "Something went wrong");

// Transform a result
var mapped = result.Map(val => $"success: {val}");         // Mapped to Result<string>
var binded = failureResult.Bind(val => BindValue(val));    // Remains a failure.

// Consume the result
var matched = mapped.Match(val => val, _ => "A failure occurred");
Console.WriteLine(matched); // Output: success: 42

if (binded.TryGetFailure(out var failure)) 
{
    Console.WriteLine(failure.Message); // Output: Something went wrong
}
```

---

## Core concepts

### What is a `Result`?

A result represents one of two possible states, either a **success** or a **failure**.
At any point, the result is either in a success state or in a failure state.

### What is a `Failure`?

A failure is a state of the `Result`, which is represented by a `Failure` record.
This record has at least the `Code` and `Message` properties, for computer and human readable information
about what caused the failure. 
It is encouraged to inherit the base `Failure` object and create specific failures for specific situations.
These inherited objects can carry additional metadata specific for the failure that occurred.
For the already provided failure overloads, see [Failures](docs/failures.md)

### What is a `Result<TValue>`?

A `Result<TValue>` is a result which carries a payload, the TValue, if the result is a success.

### Construction

Results can be constructed either by using the static factory methods, or using implicit conversion from a value or a failure.

```csharp
var a = Result.Success();               // Result, state: success
var b = Result.Failure(failure);        // Result, state: failure
var c = Result<int>.Success(42);        // Result<int>, state: success, value: 42
var d = Result<int>.Failure(failure);   // Result<int>, state: failure

Result e = failure;                     // Result, state: failure
Result<int> f = 42;                     // Result<int>, state: success, value: 42
Result<int> g = failure;                // Result<int>, state: failure
```

### Transformations

Results can be transformed on value using the many provided extension methods.
For an overview of the available methods, see [Extension methods](#extension-methods).
For a detailed description of each of the methods, see [Extension documentation](src/Results/Extensions/README.md).

### Consumption

Results can be consumed either by
- using the Match extension method which requires a delegate for both the success state as the failure state.
- using the TryGet methods, either to get the value if a success or the failure if a failure.

```csharp
var output = c.Match(
    onSuccess: value => $"success: {value}",
    onFailure: failure => failure.Message
);

Console.WriteLine(output); // Output: success: 42

if (g.TryGetFailure(out var failure))
{
    // do something with the failure.
}
```

---

## Extension Methods

The `Toarnbeike.Results.Extensions` namespace includes rich extensions for `Result` and `Result<TValue>`:

| Method                | `Result`  | `Result<T>` | Description                                      |
|-----------------------|-----------|-------------|--------------------------------------------------|
| `Bind(...)`           | [x]       | [x]         | Chains operations returning `Result<TOut>`       |
| `Map(...)`            | [ ]       | [x]         | Transforms the success value                     |
| `Tap(...)`            | [x]       | [x]         | Executes side-effects on success                 |
| `TapFailure(...)`     | [x]       | [x]         | Executes side-effects on failure                 |
| `BindTap(...)`        | [x]       | [x]         | Chains a result operation without changing value |
| `Combine(...)`        | [ ]       | [x]         | Combines two results into a new value            |
| `CombineBind(...)`    | [ ]       | [x]         | Combines two results into a new result           |
| `WithValue(...)`      | [x]       | [ ]         | Converts to `Result<TValue>` with a value        |
| `TryGetValue(...)`    | [ ]       | [x]         | Gets the success value if available              |
| `TryGetFailure(...)`  | [x]       | [x]         | Gets the failure if present                      |
| `Match(...)`          | [x]       | [x]         | Maps success or failure to a value               |

All methods support `async` variants and operate seamlessly with `Task<Result<TValue>>`.

For information per method see the [Extensions docs](docs/Extensions.md).

---

## Collections

The `Toarnbeike.Results.Collections` namespace provides extension methods for working with collections of results:

| Method              | `Result`  | `Result<T>` | Description                                 |
|---------------------|-----------|-------------|---------------------------------------------|
| `Aggregate(...)`    | [x]       | [x]         | Collects all successes or all failures      |
| `Sequence(...)`     | [ ]       | [x]         | Collects successes or returns first failure |
| `AllSuccess(...)`   | [x]       | [x]         | Checks if all results are successful        |
| `Failures(...)`     | [x]       | [x]         | Extracts all failures                       |
| `SuccessValues(...)`| [ ]       | [x]         | Extracts all success values                 |
| `Split(...)`        | [ ]       | [x]         | Splits results into successes and failures  |

All methods support `async` variants and operate seamlessly with `IEnumerable<Task<Result<TValue>>>`.

For information per method see the [Collections docs](docs/Collections.md).

---

## LINQ Query syntax support

Toarnbeike.Results supports optional integration with [C# LINQ query syntax](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/write-linq-queries),
making it easier to compose multiple `Result<TValue>` computations in a declarative style.

See the [LINQ extensions docs](docs/Linq.md) for details on how to use this feature.

---

## Test Extensions

These are ideal for unit testing and compatible with any test framework. See the [Test extensions docs](src/Results/TestHelpers/README.md) for details.

---

## Packages

The Toarnbeike.Results ecosystem consist of a couple of packages:

| Package                               | Description                                           | NuGet                                                                                                                                                   |
|---------------------------------------|-------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|
|`Toarnbeike.Results`                   | Core result type, extension methods and collections   | [![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.svg)](https://www.nuget.org/packages/Toarnbeike.Results)                                    |
|`Toarnbeike.Results.Abstractions`      | Abstractions, as netstandard2.0 project for sourceGen | [![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.Abstractions.svg)](https://www.nuget.org/packages/Toarnbeike.Results.Abstractions)                       |
|`Toarnbeike.Results.FluentValidation`  | Validation integration using `FluentValidation`       | [![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.FluentValidation.svg)](https://www.nuget.org/packages/Toarnbeike.Results.FluentValidation)  |
|`Toarnbeike.Results.MinimalApi`        | Integration with `Microsoft.AspNetCore` minimal API's | [![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.MinimalApi.svg)](https://www.nuget.org/packages/Toarnbeike.Results.MinimalApi)              |

---

## Inspiration

This project draws inspiration from:
- [Zoran Horvat (youtube)](https://www.youtube.com/@zoran-horvat)
- [Ardalis.Result (github)](https://www.nuget.org/packages/Ardalis.Result)
- [CSharpFunctionalExtensions (github)](https://www.nuget.org/packages/CSharpFunctionalExtensions)
 
---

## Conclusion

> Exceptions should be exceptional.
> Results give you clarity, safety, and composability without relying on exceptions for control flow.