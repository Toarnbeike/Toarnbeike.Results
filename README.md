![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![Code Coverage](https://toarnbeike.github.io/Toarnbeike.Results/badge_shieldsio_linecoverage_brightgreen.svg)](https://github.com/Toarnbeike/Toarnbeike.Results/blob/gh-pages/SummaryGithub.md)
[![.NET 9](https://img.shields.io/badge/.NET-9.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Results

**Toarnbeike.Results** is a lightweight, expressive C# implementation of the [Result monad](https://en.wikipedia.org/wiki/Result_type).
It enhances code clarity and reliability by enabling clear, exception-free handling of success, failure, and validation outcomes.

---

## Features

- **Explicit error handling:** Avoid exceptions for control flow; return meaningful failures instead.
- **Fluent extension methods:** Compose operations with `Bind`, `Map`, `Match`, `Tap`, and more.
- **Strongly typed generic results:** Preserve either a success value or failure details in one type-safe container.
- **Rich validation support:** Aggregate validation failures with rich property-level details.
- **Seamless integration:** All methods support asynchronous pipelines.
- **Functional programming inspired:** Inspired by FP principles for predictable, readable, and maintainable error handling.

---

## Core concepts

- **`Result`:** Represents a successful or failed operation.
- **`Result<TValue>`:** A Result carrying a value on success.
- **`Failure`:** Encapsulates error details, including validation failures.
- **`ValidationFailure`:** Describes a property-level validation issue.
- **`AggregateFailure`:** Represents grouped validation failures.

---

## Packages

| Package                               | Description                                           | NuGet                                                                                                                                                   |
|---------------------------------------|-------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|
|`Toarnbeike.Results`                   | Core option type, extension methods and collections   | [![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.svg)](https://www.nuget.org/packages/Toarnbeike.Results)                                    |
|`Toarnbeike.Results.FluentValidation`  | Validation integration using `FluentValidation`       | [![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.FluentValidation.svg)](https://www.nuget.org/packages/Toarnbeike.Results.FluentValidation)  |
|`Toarnbeike.Results.Optional`          | Integration with `Toarnbeike.Optional`                | [![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Results.Optional.svg)](https://www.nuget.org/packages/Toarnbeike.Results.Optional)                  |

---

## Inspiration

This project draws inspiration from:
- [Zoran Horvat (youtube)](https://www.youtube.com/@zoran-horvat)
- [Ardalis.Result (github)](https://www.nuget.org/packages/Ardalis.Result)
- [CSharpFunctionalExtensions (github)](https://www.nuget.org/packages/CSharpFunctionalExtensions)
 
---

## Getting Started

```bash
dotnet add package Toarnbeike.Results
```
Then import on or more of the following namespaces:
```csharp
using Toarnbeike.Results;			// Base namespace for Result, Result<TValue> and Failure
using Toarnbeike.Results.Collections;		// For extensions on IEnumerable<Result<T>>
using Toarnbeike.Results.Extensions;		// For functional extensions on Result and Result<TValue>
using Toarnbeike.Results.Failure;		// For additional failure types
using Toarnbeike.Results.Linq;		        // For LINQ query syntax support
using Toarnbeike.Results.TestHelpers;           // For fluent assertions on results. 
```

---

## Basic Usage

A result can represent either success (with or without a value) or failure (with error details):
```csharp
// Success Results
var result1 = Result.Success();						
var result2 = Result<int>.Success(42);					
var result3 = Result.Success(42);					// Type inferred
Result<int> result4 = 42;						// Implicit conversion

// Failure Results
var result4 = Result<int>.Failure(new Failure("Code", "Message"));	
Result<int> result5 = new Failure("Code", "Message");			// implicit conversion
```

Extracting the value or the failure from the rsult:
```csharp
if (result1.TryGetValue(out var value, out var failure)) 
{
	Console.WriteLine(value);					// 42
}
```

---

## Extension Methods

The `Toarnbeike.Results.Extensions` namespace includes rich extensions for `Result` and `Result<T>`:

| Method			| `Result`			 | `Result<T>`		  | Description														|
|-------------------|--------------------|--------------------|-----------------------------------------------------------------|
| `Bind(...)`		| :white_check_mark: | :white_check_mark: | Chains operations returning `Result<TOut>`						|
| `Ensure(...)`		| :x:				 | :white_check_mark: | Ensures a condition on the success value, or returns a failure	|
| `Map(...)`		| :x:				 | :white_check_mark: | Maps the success value to another type							|
| `Match(...)`		| :white_check_mark: | :white_check_mark: | Converts to another type using success/failure lambdas			|
| `Tap(...)`		| :white_check_mark: | :white_check_mark: | Executes side-effects on success								|
| `TapAlways(...)`	| :white_check_mark: | :white_check_mark: | Executes side-effects on any result                             |
| `TapFailure(...)`	| :white_check_mark: | :white_check_mark: | Executes side-effects on failure								|
| `Verify(...)`		| :white_check_mark: | :white_check_mark: | Verifies another result; propagates failure if needed			|
| `VerifyWhen(...)`	| :white_check_mark: | :white_check_mark: | Conditionally verifies another result							|
| `WithValue(...)`	| :white_check_mark: | :x:				  | Adds a value to a non-generic result							|
| `Zip(...)`		| :x:				 | :white_check_mark: | Combines two results into a `Result<(T1,T2)>`					|

All methods support `async` variants and operate seamlessly with `Task<Result<T>>`.

For information per method see the [Extensions README](src/Results/Extensions/README.md).

---

## Collections

The `Toarnbeike.Results.Collections` namespace provides extension methods for working with collections of results:

| Method			| ReturnType                                | Description														            |
|-------------------|-------------------------------------------|-------------------------------------------------------------------------------|
| `AllSuccess()`	| `bool`                                    | Returns `true` if all results in the collection are successful			    |
| `Sequence()`      | `Result<IEnumerable<T>>`                  | Returns all success values or the first encountered failure                   |
| `Aggregate()`     | `Result<IEnumerable<T>>`                  | Returns all success values or an `AggregateFailure` containing all failures   |
| `SuccessValues()` | `IEnumerable<T>`                          | Extracts all success values from a collection of results			            |
| `Failures()`      | `IEnumerable<Failure>`                    | Extracts all failures from a collection of results			                |
| `Split()`         | `(IEnumerable<T>, IEnumerable<Failure>)`  | Splits the collection into success values and failures			            |

---

## Failures

The `Toarnbeike.Results.Failures` namespace include a couple of default failures:

### `AggregateFailure`

Represents a collection of multiple failures, typically used when working with collections of `Result<T>`:
```csharp
var results = new List<Result<int>>
{
    Result<int>.Success(1),
    Result<int>.Failure(new Failure("Code1", "Message1")),
    Result<int>.Failure(new Failure("Code2", "Message2"))
};

var aggregateResult = results.Aggregate();      // Result<IEnumerable<int>>
var ggregateFailure = aggregateResult
                        .ShouldBeFailureOfType<AggregateFailure>();
```

### `ExceptionFailure`

Used when converting exceptions to failures via the `Try` factory:

``` csharp
var result1 = Result.Try(() => int.Parse("123"));	// Success(123);
var result2 = Result.Try(() => int.Parse("abc"))	// Failure(ExceptionFailure);
```

### `ValidationFailure`

Represents a property-level validation issue:

```csharp
new ValidationFailure("Email", "Email is required.");
```

### `ValidationFailures`

Aggregates multiple `ValidationFailure` instances. Typically produced using the `FluentValidation` integration.

---

## LINQ Query syntax support

Toarnbeike.Results supports optional integration with [C# LINQ query syntax](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/write-linq-queries),
making it easier to compose multiple `Result<T>` computations in a declarative style.

### Why use LINQ Query Syntax?
While method chaining works well for most scenarios, C#’s LINQ query syntax can make some workflows more expressive and readable — especially when you want to:

- Name intermediate results using `let`
- Compose complex Result pipelines in a declarative way
- Avoid deeply nested lambdas in `Bind` and `Map`

### Example:
```csharp
using Toarnbeike.Results.Linq;

var result =
    from id in GetUserId()
    from user in GetUserById(id)
    let fullName = $"{user.FirstName} {user.LastName}"
    select new UserDto(fullName, user.Email);
```

When comparing that with method chaining, the LINQ query syntax can be more readable, especially for complex workflows:
```csharp
var result = GetUserId()
    .Bind(GetUserById)
    .Map(user =>
    {
        var name = $"{user.FirstName} {user.LastName}";
        return new UserDto(name, user.Email);
    });
```
Which also works, but makes name only available inside the `Map` lambda.

---

## TestHelpers

These are ideal for unit testing and compatible with any test framework. See the [TestHelpers README](src/Results/TestHelpers/README.md) for details.

---

## Why Results?

> Exceptions should be exceptional.
> Result<T> gives you clarity, safety, and composability — without relying on exceptions for control flow.
