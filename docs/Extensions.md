# Toarnbeike.Results.Extensions

This package (or namespace) provides **fluent extension methods** for `Result` and `Result<TValue>` to help write expressive and composable code.

These extensions are inspired by functional programming concepts like monads and enable a clean and declarative approach to handling success/failure flows.

---

## Overview

| Method                                 | `Result`  | `Result<T>` | Description                                      |
|----------------------------------------|-----------|-------------|--------------------------------------------------|
| [`Bind(...)`](#bind)                   | [x]       | [x]         | Chains operations returning `Result<TOut>`       |
| [`Map(...)`](#map)                     | [ ]       | [x]         | Transforms the success value                     |
| [`Tap(...)`](#tap)                     | [x]       | [x]         | Executes side-effects on success                 |
| [`TapFailure(...)`](#tapfailure)       | [x]       | [x]         | Executes side-effects on failure                 |
| [`BindTap(...)`](#bindtap)             | [x]       | [x]         | Chains a result operation without changing value |
| [`Combine(...)`](#combine)             | [ ]       | [x]         | Combines two results into a new value            |
| [`CombineBind(...)`](#combinebind)     | [ ]       | [x]         | Combines two results into a new result           |
| [`WithValue(...)`](#withvalue)         | [x]       | [ ]         | Converts to `Result<TValue>` with a value        |
| [`TryGetValue(...)`](#trygetvalue)     | [ ]       | [x]         | Gets the success value if available              |
| [`TryGetFailure(...)`](#trygetfailure) | [x]       | [x]         | Gets the failure if present                      |
| [`Match(...)`](#match)                 | [x]       | [x]         | Maps success or failure to a value               |

---

## Bind

Projects a successful `Result` into a new `Result<TOut>` using a chained result:
``` csharp
Result<int> step1 = GetId();
Result<User> result = step1.Bind(GetUserById);
```
The second result might depend on the value of the first.
If the original result is a failure, that failure is returned.
Otherwise, the second result is returned.

---

## Map

Transforms the success value of a `Result<T>` into a new value:
``` csharp
Result<int> result = GetNumber();
Result<string> mapped = result.Map(x => x.ToString());
```
The mapping function is only executed on success.
If the original result is a failure, that failure is returned unchanged.
Otherwise, the mapped value is wrapped in a new `Result<TOut>`.

---

## Tap

Executes a side-effect when the result is successful:
``` csharp
Result<User> result = GetUser();
result.Tap(user => logger.Log(user));
```
The action is only executed on success.
The original result is returned unchanged.

---

## TapFailure

Executes a side-effect when the result is a failure:
``` csharp
Result<User> result = GetUser();
result.TapFailure(error => logger.Log(error));
```
The action is only executed on failure.
The original result is returned unchanged.

---

## BindTap

Chains a result-producing operation without changing the original value:
``` csharp
Result<User> result =
    GetUser()
        .BindTap(user => Audit(user));
```
The chained operation may fail and short-circuit the pipeline.
If the original result is a failure, that failure is returned.
If the chained operation fails, its failure is returned.
Otherwise, the original value is preserved.

---

## Combine

Combines two successful results into a new value:
``` csharp
Result<User> user = GetUser();
Result<Permissions> permissions = GetPermissions();

Result<UserContext> result =
    user.Combine(permissions, (u, p) => new UserContext(u, p));
```
Both results are evaluated independently.
If either result is a failure, that failure is returned.
Otherwise, the projector is applied and its result is wrapped.

---

## CombineBind

Combines two successful results into a new `Result<TOut>`:
``` csharp
Result<User> user = GetUser();
Result<Permissions> permissions = GetPermissions();

Result<UserContext> result =
    user.CombineBind(permissions, CreateContext);
```
The projector returns a `Result<TOut>`.
If either input result is a failure, that failure is returned.
Otherwise, the projector result is returned directly.

---

## WithValue

Converts a non-generic `Result` into a `Result<TValue>`.
``` csharp
Result result = Validate();
Result<int> valued = result.WithValue(42);
```
If the original result is a failure, that failure is returned.
Otherwise, the provided value is wrapped in a successful result.

---

## TryGetValue
Attempts to get the success value from a `Result<T>`.
``` csharp
if (result.TryGetValue(out var value))
{
    // use value
}
```
Returns `true` if the result is successful.
Returns `false` if the result is a failure.

---

## TryGetFailure
Attempts to retrieve the failure from a `Result` or `Result<T>`.
``` csharp
if (result.TryGetFailure(out var failure))
{
    // handle failure
}
```
Returns `true` if the result is a failure.
Returns `false` if the result is successful.

---

## Match 

Projects a result into a value using success and failure functions:
``` csharp
string message = result.Match(
    onSuccess: value => $"Success: {value}",
    onFailure: error => $"Error: {error}");
);
```
Exactly one of the functions is executed.
The result of that function is returned.

---

## Notes

- All extension methods have overloads for `Task<Result<T>>`, allowing fluent async composition.
- Extension methods are located in the Toarnbeike.Results.Extensions namespace.
- Each method short-circuits on failure and only proceeds on success (as expected in functional chains).