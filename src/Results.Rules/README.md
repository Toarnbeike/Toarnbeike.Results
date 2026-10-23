![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](../../LICENSE)

# Toarnbeike.Results.Rules

`Toarnbeike.Results.Rules` adds rule-based `Ensure` and `Validate` extensions to `Result` and `Result<T>`.

Select a value or property, apply a built-in rule, and receive either the original successful result or a typed failure that explains why the rule did not pass.

The library provides rules for common .NET types, generated and customizable failure messages, configurable floating-point tolerances, and process-wide evaluation settings through `RuleContext`.

---

## Contents

1. [Quick start](#quick-start)
1. [Ensure vs Validate (semantic difference)](#ensure-vs-validate-semantic-difference)
1. [Ensure extensions](#ensure-extensions)
1. [Validate extensions](#validate-extensions)
1. [Collection rules](#collection-rules)
1. [Available rule groups and methods](#available-rule-groups-and-methods)
1. [Failure messages](#failure-messages)
1. [Floating-point tolerance](#floating-point-tolerance)
1. [Rule context](#rule-context)
1. [Detailed rules reference](#detailed-rules-reference)

---

## Quick start

```csharp
using Toarnbeike.Results;
using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Validation;

Result<Customer> customer = GetCustomer();

var guarded = customer
	.Ensure(c => c.Id).NotEmpty()
	.Ensure(c => c.Age).AtLeast(18);

var validated = customer
	.Validate(c => c.Email).NotEmpty()
	.ValidateCollection(c => c.Tags).NotEmpty();
```

Each rule can take a custom message:

```csharp
var guarded = customer.Ensure(c => c.Id).NotEmpty("Customer id is required.");
var validated = customer.Validate(c => c.Email).NotEmpty("Email is required.");
```

Both APIs short-circuit at the first failed rule in the current chain step.

---

## Ensure vs Validate (semantic difference)

Both APIs execute rules, but they represent different intent and produce different failure types.

### Ensure

Use `Ensure` to enforce **domain invariants**. A failed ensure returns `GuardFailure`.

`GuardFailure` derives from `DomainFailure`, and `DomainFailure` uses `FailureCategory.Business`.

This is appropriate when the operation itself must be rejected because a business or domain rule is not satisfied. For example, a domain operation can use `Ensure` before accepting a state transition. The operation returns a business failure rather than creating or accepting an invalid domain state.

### Validate

Use `Validate` for **input validation** (request DTOs, form data, external payloads). A failed validate returns `ValidationFailure`.

`ValidationFailure` uses `FailureCategory.Validation` and carries:
- `Property` (which input field failed)
- `ValidationMessage` (what was wrong)

This is appropriate when callers are expected to correct input and retry.

| API | Typical layer | Failure type | Failure category | Intended meaning |
|---|---|---|---|---|
| `Ensure` | Domain | `GuardFailure` (`DomainFailure`) | `Business` | Domain rule violation; operation must not proceed. |
| `Validate` | Application boundary | `ValidationFailure` | `Validation` | Input is invalid; report actionable field-level feedback. |

---

## Ensure extensions

`Ensure` applies a rule to the value in a `Result<T>`, a selected property, or a selected collection. The selector expression supplies the property name for the resulting `GuardFailure`; provide `propertyName` when that inferred name is unsuitable. A successful ensure retains the original `Result<T>` value.

`Result` also supports `Ensure` and `EnsureCollection` with a value selector. This lets an operation without a value apply a guard to a value available in its surrounding scope.

---

## Validate extensions

`Validate` applies a rule to the value in a `Result<T>` or to a selected property. `ValidateCollection` targets a selected `IEnumerable<TItem>`. The selector expression supplies the `ValidationFailure.Property` value; provide `propertyName` when an explicit property name is required.

The validation operation succeeds without changing the contained value. On failure, it returns one `ValidationFailure` for the first failed rule.

---

## Collection rules

Use `EnsureCollection` or `ValidateCollection` to select an `IEnumerable<TItem>`. The returned target supports both rules about the collection itself and rules about its items.

### Rules about the collection

Apply collection rules directly to require a collection to contain an item or to constrain its size:

```csharp
var guarded = order.EnsureCollection(o => o.Lines)
	.NotEmpty()
	.HasAtMost(100);

var validated = request.ValidateCollection(r => r.Tags)
	.HasBetween(1, 5);
```

### Rules about collection items

Call `All()` to require every item to pass the following rule. Call `Any()` to require at least one item to pass it. Both methods have an overload that selects a property from each item.

```csharp
var guarded = order
	.EnsureCollection(o => o.Lines)
	.All(line => line.Quantity).AtLeast(1);

var validated = request
	.ValidateCollection(r => r.Tags)
	.All().NotWhiteSpace();

var hasAnEnabledLine = order
	.EnsureCollection(o => o.Lines)
	.Any(line => line.IsEnabled).EqualTo(true);
```

`All` evaluates items in enumeration order and stops at the first failed item. `Any` stops at the first item that passes; it fails when no item passes. The resulting failure remains associated with the selected collection property.

---

## Available rule groups and methods

The following fluent methods are available from `Toarnbeike.Results.Rules.Extensions`.

| Rule group | Applicable target type | Methods |
|---|---|---|
| String | `string` | `NotEmpty`, `NotWhiteSpace`, `MinLength`, `MaxLength`, `LengthBetween`, `MatchesRegex` |
| Collection | `IEnumerable<T>` | `Contains`, `NotContains`, `NotEmpty`, `Empty`, `HasAtLeast`, `HasAtMost`, `HasBetween`, `HasExactly` |
| Date | `DateOnly`, `DateTime`, `DateTimeOffset` | `OnOrAfter`, `OnOrBefore`, `Between` |
| TimeSpan | `TimeSpan` | `AtLeast`, `AtMost`, `Between`, `Around` |
| Enum | `enum` | `IsDefined`, `OneOf`, `NotOneOf` |
| Guid | `Guid` | `NotEmpty`, `Version4`, `Version7` |
| Integer | `IBinaryInteger<T>` | `AtLeast`, `AtMost`, `Between`, `MultipleOf` |
| Numeric | `INumber<T>` | `GreaterThan`, `LessThan`, `Positive` |
| Floating point | `IFloatingPoint<T>` | `AtLeast`, `AtMost`, `Between`, `MultipleOf`, `Finite` |
| Object | Any property type | `EqualTo`, `NotEqualTo`, `Must`, `MustNot` |

Notes:
- Floating-point comparison methods accept an optional tolerance argument.
- Object rules apply to every property type. `Must` and `MustNot` require an explicit failure message; `EqualTo` and `NotEqualTo` use equality semantics for the target type.

---

## Failure messages

Built-in rules provide English failure messages by default. A message supplied as the final rule argument takes precedence over the generated message:

```csharp
var result = customer.Validate(c => c.Email)
	.NotEmpty("Email is required.");
```

Generated messages are based on the failed rule and values captured during its evaluation, such as the attempted value, a comparison boundary, or an expected collection item. Values are formatted using the culture configured through `RuleContext`.

To localize or replace generated messages, implement `IRuleMessageProvider` and configure it once for the process:

```csharp
using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Rules.Messages;

IRuleMessageProvider messages = new MyRuleMessageProvider();
RuleContext.UseCustomMessageProvider(messages);
```

The provider maps rule identifiers to templates. Templates can use placeholders supplied by the rule evaluation, such as `{attemptedValue}`, `{min}`, `{max}`, `{target}`, and `{tolerance}`. If a failed rule has no template, the library returns a diagnostic fallback message containing the rule, evaluator, attempted value, and evaluation context.

---

## Floating-point tolerance

The floating-point `AtLeast`, `AtMost`, `Between`, and `MultipleOf` rules accept an optional tolerance. When omitted, they obtain a tolerance from `RuleContext`.

```csharp
var result = measurement.Ensure(m => m.Value)
	.Between(9.99, 10.01, tolerance: 0.001);
```

The default tolerances are:

| Type | Default tolerance |
|---|---:|
| `float` | `1e-6` |
| `double` | `1e-9` |
| `decimal` | `1e-9` |
| `Half` | `1e-2` |

Use `RuleContext.UseZeroTolerance()` for exact comparisons, `UseDefaultTolerance()` to restore the defaults, or `UseCustomToleranceProvider(...)` to supply tolerances for numeric types. A negative tolerance is rejected. The `GreaterThan`, `LessThan`, and `Positive` numeric rules are strict and never use tolerance.

---

## Rule context

`RuleContext` holds process-wide settings used while rules are evaluated. Configure it during application startup or isolate and restore settings in tests; changing it affects subsequent rule evaluations throughout the process.

```csharp
using System.Globalization;
using Toarnbeike.Results.Rules;

RuleContext.UseCulture(CultureInfo.GetCultureInfo("nl-NL"));
RuleContext.UseFixedUtcNow(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero));
RuleContext.UseZeroTolerance();
```

| Configuration | Effect |
|---|---|
| `UseDefaultMessageProvider()` | Restores the built-in English message templates. |
| `UseCustomMessageProvider(provider)` | Uses message templates supplied by the application. |
| `UseInvariantCulture()`, `UseCulture(culture)`, `UseSystemCulture()` | Controls culture-sensitive formatting of values in generated messages. |
| `UseDefaultTolerance()`, `UseZeroTolerance()`, `UseCustomToleranceProvider(provider)` | Controls the default tolerance used by floating-point rules when no explicit tolerance is supplied. |
| `UseSystemUtcNow()`, `UseFixedUtcNow(value)` | Controls the current UTC time used by time-relative date rules, allowing deterministic tests. |

---

## Detailed rules reference

For the detailed behavior of each rule, see [docs/Rules.md](../../docs/Rules.md).
