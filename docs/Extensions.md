# Toarnbeike.Results.Extensions

This package (or namespace) provides **fluent extension methods** for `Result` and `Result<TValue>` to help write expressive and composable code.

These extensions are inspired by functional programming concepts like monads and enable a clean and declarative approach to handling success/failure flows.

---

## Overview

| Method			                | `Result`		| `Result<TValue>` | Description													|
|-----------------------------------|---------------|------------------|----------------------------------------------------------------|
| [`Bind(...)`](#bind)	            | ✔	            | ✔	               | Chains operations returning `Result<TOut>`						|
| [`Check(...)`](#check)		    | ✖				| ✔	               | Check a condition on the success value, or returns a failure	|
| [`Map(...)`](#map)		        | ✖				| ✔	               | Maps the success value to another type							|
| [`Match(...)`](#match)		    | ✔	            | ✔	               | Converts to another type using success/failure lambdas			|
| [`Tap(...)`](#tap)		        | ✔	            | ✔	               | Executes side-effects on success								|
| [`TapAlways(...)`](#tap)	        | ✔	            | ✔	               | Executes side-effects on any result                            |
| [`TapFailure(...)`](#tap)	        | ✔	            | ✔	               | Executes side-effects on failure								|
| [`Verify(...)`](#verify)		    | ✔	            | ✔	               | Verifies another result; propagates failure if needed			|
| [`VerifyWhen(...)`](#verify)      | ✔	            | ✔	               | Conditionally verifies another result							|
| [`WithValue(...)`](#withValue)	| ✔	            | ✖	               | Adds a value to a non-generic result							|
| [`Zip(...)`](#zip)       		    | ✖	            | ✔	               | Combines two results into a `Result<(T1,T2)>`					|

---

## Bind

Projects a successful `Result` into a new `Result<TOut>` using a chained Result:
``` csharp
Result<int> step1 = GetId();
Result<User> result = step1.Bind(GetUserById);
```
The second result might depend on the value of the first.
If the original result is a failure, that failure is returned.
Otherwise, the second result is returned.

---

## Check

Check that a condition on the initial result value is met, otherwise returns a failure.
``` csharp
var result = Result.Success(42)
    .Check(x => x > 0, () => new Failure("Negative", "Value must be positive"));
```

---

## Map

Maps the value inside a success result to another type.
``` csharp
var result = Result.Success(1.3m)
    .Map(x => (int)(x * 10)); // Result<int> with value 13
```

---

## Match 

Converts a result to a new value by pattern matching on its state.
``` csharp
string message = result.Match(
    success => $"User ID: {success}",
    failure => $"Error: {failure.Message}"
);
```

---

## Tap

Applies side effects without modifying the result.
``` csharp
result
    .Tap(user => _logger.Log($"User found: {user.Name}"))
    .TapFailure(error => _logger.LogError(error.Message))
    .TapAlways(() => _logger.Log("Pipeline finished"));
```

---

## Verify - Obsolete, will be replaced by BindTap

Verifies additional conditions or results without modifying the value.
``` csharp
result.Verify(ValidateBusinessRules());
result.VerifyWhen(condition, ValidateExtraStep());
```

---

## WithValue - Obsolete, will be replaced with Map

Attaches a value to a non-generic `Result` to make a `Result<T>`.
``` csharp
var result = Result.Success()
    .WithValue(42);
```

---

## Zip - Obsolete, can be achieved with Bind or Map and separate methods

Combines two results into one result with a tuple of values.
``` csharp
var r1 = Result.Success(1);
var r2 = Result.Success("Hello");
var zipped = r1.Zip(r2); // Result<(int, string)>
```

---

## Notes

- All extension methods have overloads for `Task<Result<T>>`, allowing fluent async composition.
- Extension methods are located in the Toarnbeike.Results.Extensions namespace.
- Each method short-circuits on failure and only proceeds on success (as expected in functional chains).