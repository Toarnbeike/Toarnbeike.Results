# Toarnbeike.Results.Extensions

This package (or namespace) provides **fluent extension methods** for `Result` and `Result<TValue>` to help write expressive and composable code.

These extensions are inspired by functional programming concepts like monads and enable a clean and declarative approach to handling success/failure flows.

---

## Overview

| Method			                | `Result`			 | `Result<T>`		  | Description														|
|-----------------------------------|--------------------|--------------------|-----------------------------------------------------------------|
| [`Bind(...)`](#bind)	            | :white_check_mark: | :white_check_mark: | Chains operations returning `Result<TOut>`						|
| [`Ensure(...)`](#ensure)		    | :x:				 | :white_check_mark: | Ensures a condition on the success value, or returns a failure	|
| [`Map(...)`](#map)		        | :x:				 | :white_check_mark: | Maps the success value to another type							|
| [`Match(...)`](#match)		    | :white_check_mark: | :white_check_mark: | Converts to another type using success/failure lambdas			|
| [`Tap(...)`](#tap)		        | :white_check_mark: | :white_check_mark: | Executes side-effects on success								|
| [`TapFailure(...)`](#tap)	        | :white_check_mark: | :white_check_mark: | Executes side-effects on failure								|
| [`Verify(...)`](#verify)		    | :white_check_mark: | :white_check_mark: | Verifies another result; propagates failure if needed			|
| [`VerifyWhen(...)`](#verify)      | :white_check_mark: | :white_check_mark: | Conditionally verifies another result							|
| [`WithValue(...)`](#withValue)	| :white_check_mark: | :x:				  | Adds a value to a non-generic result							|
| [`Zip(...)`](#zip)       		    | :x:				 | :white_check_mark: | Combines two results into a `Result<(T1,T2)>`					|

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

## Ensure

Ensures that a condition on the initial result value is met, otherwise returns a failure.
``` csharp
var result = Result.Success(42)
    .Ensure(x => x > 0, () => new Failure("Negative", "Value must be positive"));
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
    .TapFailure(error => _logger.LogError(error.Message));
```

---

## Verify

Verifies additional conditions or results without modifying the value.
``` csharp
result.Verify(ValidateBusinessRules());
result.VerifyWhen(condition, ValidateExtraStep());
```

---

## WithValue

Attaches a value to a non-generic `Result` to make a `Result<T>`.
``` csharp
var result = Result.Success()
    .WithValue(42);
```

---

## Zip

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