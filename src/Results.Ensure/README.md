![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Results.Ensure

Composable guard library for C# that enables clean, fail-fast domain validation without exceptions.

## Features

- Static Ensure API: `Ensure.GreaterThan(value, 10);` 
- Extension methods on primitives: `value.GreaterThan(10)`
- Validation pipeline: `Result.Ensure(() => value.GreaterThan(10))`

---

## Contents
1. [Quick start](#quick-start)
1. [Core concepts](#core-concepts)
1. [Extensions](#extensions)
1. [Design principles](#design-principles-and-best-practices)
1. [Conclusion](#conclusion)

---

## Quick start

Start a ensure pipeline using `Result.Ensure`. Apply any additional business rules using additional `Ensure()` calls:

``` csharp
public static Result<Order> Create(
	string email,
	IEnumerable<Sku> items,
	DateOnly shippingDate)
{
	return Result
		.Ensure(() => email.NotWhiteSpace())
		.Ensure(() => items.NotEmpty())
		.Ensure(() => shippingDate.After(DateOnly.FromDateTime(DateTime.Today)))
		.WithValue(() => new Order(email, items, shippingDate))
}
```

The result is either a success with the created Order, or a `GuardFailure`. The pipeline is **fail-fast**, so only the first failure is reported.

This makes the approach useful for domain rules, not for input validation. It is advised to use an external package, e.g. `FluentValidation` for input validation.

---

## Core concepts

### Installation

``` bash
dotnet add package Toarnbeike.Results.Ensure
```

### Static Ensure API
Useful for single domain checks during execution:
``` csharp
Result<DateTime> result = Ensure.After(date, comparison);
```
Does not pollute the primitive with additional extension methods.

### Extension methods on primitives
Useful for ensure pipelines:
``` csharp
Result<DateTime> result = date.After(comparison);
``` 
Returns exactly the same as `Ensure.After`, but is cleaner and more readable in a ensure pipeline.

### Ensure pipelines
The library provides a pipeline operator on Result:
```csharp
Result result = Result
				.Ensure(() => rule1)
				.Ensure(() => rule2);
```
This enables a **fail-fast pipeline** without passing values around.

### `GuardFailure`
A failure from the extension methods or the static Ensure API returns a `GuardFailure`.

This failure provides additional information regarding the failure:
- `Message`: English message regarding the failure (can be customized using message parameter in extension methods)
- `GuardName`: Name of the guard that caused the failure
- `Constraint`: Value(s) the value what compared against, e.g. the minLength parameter.
- `AttemptedValue`: The value that caused the failure
- `ParameterName`: The parameter name of the failed value. Attached automatically using `[CallerArgumentExpression]`.

---

## Extensions

The following guards are currently provided:

| Method                        | Target            | Description                                                   | Strategy      |
|-------------------------------|-------------------|---------------------------------------------------------------|---------------|
| NotEmpty()                    | `string`          | Verifies string is not empty                                  |               |
| NotWhiteSpace()               | `string`          | Verifies string is not white space                            |               |
| MinLength(`int`)              | `string`          | Verifies minimum string length (inclusive)                    | inclusive     |
| MaxLength(`int`)              | `string`          | Verifies maximum string length (inclusive)                    | inclusive     |
| Matches(`Regex`)              | `string`          | Verifies string matches provided regular expression           |               |
| GreaterThan(`TNumber`)        | `TNumber`         | Verifies value is greater than comparison (exclusive)         | exclusive     |
| LessThan(`TNumber`)           | `TNumber`         | Verifies value is less than comparison (exclusive)            | exclusive     |
| InRange(`TNumber`,`TNumber`)  | `TNumber`         | Verifies value is between provided range (inclusive)          | inclusive     |
| After(`TDate`)                | `TDate`           | Verifies date (`DateTime`, `DateOnly`) is after comparison    | inclusive     |
| Before(`TDate`)               | `TDate`           | Verifies date (`DateTime`, `DateOnly`) is before comparison   | inclusive     |
| NotEmpty()                    | `IEnumerable<>`   | Verifies collection contains at least one element             |               |
| IsDefined()                   | `enum`            | Verifies provided element is defined within the `enum`        |               |

### Custom guards

You can provide your own guards as extension methods on the primitives:

``` csharp
public static Result<DateOnly> OnTuesday(this DateOnly date, [CallerArgumentExpression(nameof(date))] string expr = null) 
{
	return date.DayOfWeek == DayOfWeek.Tuesday 
		? date 
		: new GuardFailure(nameof(OnTuesday), "'date' should be on a Tuesday.", expr); 
}
```

For rules involving multipe values:

``` csharp
private static Result VerifyPostalCode(string postalCode, string country)
{
      return country switch
	  {
		"NL" => postalCode.Matches(nlPostalCodeRegex),
		"D" => postalCode.Matches(dPostalCodeRege),
		_ => Result.Success() // no verification for these countries.
	  }
}
```

Used in a pipeline:
``` csharp
.Ensure(() => date.OnTuesday())
.Ensure(() => VerifyPostalCode(code, country));
```

---

## Design principles and best practices

- No exceptions: all guards are explicit and composable
- Fail fast: no aggregation of failures, pipeline stops at first failure
- No data flow: validation does not require passing values through the pipeline
- Domain first: rules live on the types they belong to

### When to use:
When failures are not expected, but must be enforced:
- Domain object creation
- Business rules
- Value object invariants

### When not to use
When failures are expected, and aggregated to inform user:
- Application layer validation
- DTO validation
- User input validation

Use `FluentValidation` instead.

---

## Conclusion

`Toarnbeike.Results.Ensure` enables:
- Clean domain factories
- Reusable guard logic
- Functional error handling