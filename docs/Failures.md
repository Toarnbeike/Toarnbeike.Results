## Failures

This document describes the concept of failures in the `Toarnbeike.Results` library.
These failures are **subject to change** starting from [version 2.0](Future.md), as we plan to introduce a more structured approach to representing errors.

Failures are used to represent error conditions in a structured way. 
A failure typically contains a code and a message, and can be extended with additional properties as needed.

## Content

1. [AggregateFailure](#aggregatefailure)
1. [ExceptionFailure](#exceptionfailure)
1. [ValidationFailure](#validationfailure)
1. [ValidationFailures](#validationfailures)

---

### `AggregateFailure`

Represents a collection of multiple failures, typically used when working with collections of `Result<TValue>`:
```csharp
var results = new List<Result<int>>
{
    Result<int>.Success(1),
    Result<int>.Failure(new Failure("Code1", "Message1")),
    Result<int>.Failure(new Failure("Code2", "Message2"))
};

var aggregateResult = results.Aggregate();      // Result<IEnumerable<int>>
var agregateFailure = aggregateResult
                        .ShouldBeFailureOfType<AggregateFailure>();
```

---

### `ExceptionFailure`

Used when converting exceptions to failures via the `Try` factory:

``` csharp
var result1 = Result.Try(() => int.Parse("123"));	// Success(123);
var result2 = Result.Try(() => int.Parse("abc"))	// Failure(ExceptionFailure);
```

Result.Try() also has async variants for `Func<Task>`, `Func<Task<T>>`, `Func<ValueTask>` and `Func<ValueTask<T>>`.

---

### `ValidationFailure`

Represents a property-level validation issue:

```csharp
new ValidationFailure("Email", "Email is required.");
```

---

### `ValidationFailures`

Aggregates multiple `ValidationFailure` instances. Typically produced using the `FluentValidation` integration.

---