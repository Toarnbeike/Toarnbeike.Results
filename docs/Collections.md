# Toarnbeike.Results.Collections

This namespace provides **fluent extension methods** for `IEnumerable<Result>` and `IEnumerable<Result<TValue>>` to help write expressive and composable code.

These extensions are inspired by functional programming concepts like monads and enable a clean and declarative approach to handling success/failure flows on collections of results.

---

## Overview

| Method                                | `Result`  | `Result<T>` | Description                                 |
|---------------------------------------|-----------|-------------|---------------------------------------------|
| [`Aggregate(...)`](#aggregate)        | [x]       | [x]         | Collects all successes or all failures      |
| [`Sequence(...)`](#sequence)          | [ ]       | [x]         | Collects successes or returns first failure |
| [`AllSuccess(...)`](#allsuccess)      | [x]       | [x]         | Checks if all results are successful        |
| [`Failures(...)`](#failures)          | [x]       | [x]         | Extracts all failures                       |
| [`SuccessValues(...)`](#successvalues)| [ ]       | [x]         | Extracts all success values                 |
| [`Split(...)`](#split)                | [ ]       | [x]         | Splits results into successes and failures  |

See [Aggregates vs sequence](#aggregates-vs-sequence) for guidance on when to use which method.

---

## Aggregate

Collects all success values or all failures:
``` csharp
IEnumerable<Result<int>> results = GetResults();
Result<IEnumerable<int>> aggregated = results.Aggregate();
```
All results are evaluated.
If any failures occur, all failures are collected into a single AggregateFailure.
Otherwise, all success values are returned.

This method is useful when:
- you want full error reporting
- partial success is not sufficient
- all failures must be known

---

## Sequence

Collects all success values or returns the first failure::
``` csharp
IEnumerable<Result<int>> results = GetResults();
Result<IEnumerable<int>> sequenced = results.Sequence();
```
Results are evaluated in order.
If a failure occurs, that failure is returned immediately.
Otherwise, all success values are returned.

This method is useful when:
- you want fail-fast behavior
- later results depend on earlier success
- performance matters (early exit)

---

## Aggregates vs sequence

| Behaviour		   | `Aggregate`                            | `Sequence`                            |
|------------------|----------------------------------------|---------------------------------------|
| Failures         | Collects all into an AggregateFailure  | Returns the first failure encountered |
| Evaluation       | Evaluates all results                  | Evaluates until first failure         |
| Use case         | Validation/reporting                   | Pipelines/execution                   |

---

## AllSuccess

Checks whether all results are successful:
``` csharp
IEnumerable<Result<int>> results = GetResults();
bool allSuccess = results.AllSuccess();
```
Returns `true` if all results are successful.
Returns `false` if any result is a failure.

---

## Failures

Extracts all failures from a sequence:
``` csharp
IEnumerable<Result> results = GetResults();
IEnumerable<Failure> failures = results.Failures();
```
Only failure values are returned.
Successful results are ignored.

---

## SuccessValues

Extracts all success values from the sequence:
``` csharp
IEnumerable<Result<int>> results = GetResults();
IEnumerable<int> successValues = results.SuccessValues();
```
Only success values are returned.
Failures are ignored.

---

## Split

Splits results into successes and failures:
``` csharp
IEnumerable<Result<int>> results = GetResults();
var (values, failures) = results.Split();	// values: IEnumerable<int>, failures: IEnumerable<Failure>
```
All results are evaluated.
Success values and failures are returned as separate collections.
Order is preserved within each collection.

---

## Notes

- All extension methods have overloads for `Task<IEnumerable<Result<T>>>`, allowing fluent async composition.
- Extension methods are located in the Toarnbeike.Results.Collections namespace.