![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](../../LICENSE)

# Toarnbeike.Results.Validation

This package extends **Toarnbeike.Results** with two complementary ways to validate values wrapped in a `Result<T>`:
- **Fluent Validation**: for short-circuiting, property-level validation using a fluent API.
- **`IValidator`**: for collecting all validation failures into a single validation result.

Both approaches are designed to integrate seamlessly with the `Result` type, enabling clear, declarative validation pipelines that compose naturally with other `Result` operations.

---

## Contents

1. [Quick start](#quick-start)
1. [Core concepts](#core-concepts)
1. [Fluent validation](#fluent-validation)
1. [IValidator](#ivalidator)
1. [Validation rules](#validation-rules)
1. [Customization](#customization)

---

## Quick start

Use inline validation rules on a `Result<T>` pipeline for short-circuiting validation:

```csharp
public class Person
{
	public string Name { get; set; }
	public int Age { get; set; }
	public List<string> Tags { get; set; }
}

Result<Person> personResult = GetPerson();
personResult
	.Validate(p => p.Name).MinLength(3)
	.Validate(p => p.Age).GreaterThan(18)
	.ValidateAll(p => p.Tags).MinLength(3)
	.Match(
		onSuccess: person => $"Valid: {person.Name}",
		onFailure: failure => $"Invalid: {failure.ValidationMessage}"
	);
```

Or define a reusable validator for domain-specific logic:
```csharp
public class PersonValidator : Validator<Person>
{
	public override void RegisterRules(ValidationRuleCollection<Person> rules)
	{
		rules.That(p => p.Name).MinLength(3).MatchesRegex(@"^[a-zA-Z]+$");
		rules.That(p => p.Age).GreaterThan(18);
		rules.ThatAll(p => p.Tags).MinLength(3);
	}
}

// Usage:
personResult
	.ValidateUsing(new PersonValidator()); // (or use dependency injection to resolve the validator)
```

---


## Core concepts

### What is Fluent validation

With the Validate extension methods, you can declare validation rules inline on a `Result<T>` pipeline. 
Each rule is applied to a specific property of the object, and validation short-circuits on the first failure for that property.

### What is a `Validator<T>`

Define a reusable validator by implementing the `Validator<T>` base class.
The class requires you to implement the `RegisterRules` method, where you can declare validation rules for the target type.
This allows you to encapsulate validation logic for a specific type and reuse it across your application. The validator collects all validation failures into a single result, which can be consumed by the caller.

### What are the results?

The fluent validation methods returns a `ValidationFailure` result when a validation rule fails.
This result contains the property name, and the auto generated or custom validation message.

The IValidator approach returns a `ValidationFailureSummary` when any validation rule fails.
This summary contains a dictionary of all `ValidationFailure` results per property for the object, allowing you to retrieve all validation issues at once.

### What is validation in Results.Validation?

Validation is the process of checking that an object's properties satisfy specified constraints. **Results.Validation** provides a type-safe, fluent API for declaring and enforcing these constraints.

A validation pipeline is built by combining:
1. A **property selector** (e.g., `p => p.Name`) - identifies which property to validate
2. **Validation rules** (e.g., `.NotEmpty()`, `.MinLength(3)`) - define the constraints
3. **Result consumption** - extract success or failure information

---

## Fluent validation

The `Validate` extension starts a validation pipeline for a selected property.

Validation rules can be applied to properties, nested properties, or collection.
``` csharp
Result<Person> result = GetPerson();

var validated = result 
	.Validate(x => x.Name).MinLength(3)
	.Validate(x => x.Age).AtLeast(18)
	.Validate(x => x.Address.PostalCode).Matches(@"^\d{5}$")
	.ValidateAll(x => x.Tags).MinLength(3);
```

The property expression is used to determine the validation property name. For example, `x => x.Address.PostalCode` produces the property name `Address.PostalCode`.

### Short-circuiting validation

The fluent-api is designed to short-circuit validation on the first failure for a property. If a rule fails, subsequent rules for that property are skipped, and the failure is returned immediately.
Consequently, only the first validation failure is reported for each property, and a single `ValidationFailure` is returned as result of a failing validation pipeline.

This makes the fluent API particularly useful when validation is part of a larger `Result<T>` pipeline where subsequent operations should only execute when the previous operation succeeded.

## IValidator

When all validation rules for a type should be evaluated, implement the `IValidator<T>` interface.
```csharp
var validated = result.ValidateUsing(personValidator);
```

A validator defines it rules in the `RegisterRules` method, where you can declare multiple rules for each property of the target type.
``` csharp
public sealed class CustomerValidator(ICustomerRepository repository) : Validator<Customer> 
{ 
	protected override void RegisterRules(ValidationRuleCollection<Customer> rules)
	{ 
		rules.That(x => x.Name).MinLength(3).Matches(@"^[a-zA-Z]+$");
		rules.That(x => x.Age).AtLeast(18); 
		rules.ThatAll(x => x.Tags).MinLength(5); 
	} 
}
```

The validator is intended to contain the complete validation definition for a type. Dependencies can be injected normally through the constructor.

### Aggregating validation

Unlike the fluent API, an `IValidator<T>` evaluates all rules for a type and aggregates the results into a single `ValidationFailureSummary`. 
This allows you to report multiple validation issues at once, rather than short-circuiting on the first failure.

`ValidationFailureSummary` groups validation failures by property name, allowing you to easily identify which properties failed and why.

## Validation rules

`Results.Validation` provides pre-built rules organized by data type. Each category includes multiple validators covering common constraints.

| Category | Types | Methods |
|----------|-------|---------|
| **String** | `string` | NotEmpty, NotWhiteSpace, MinLength, MaxLength, LengthBetween, Matches |
| **Collection** | `IEnumerable<T>` | NotEmpty, Empty, HasAtLeast, HasAtMost, HasBetween, HasExactly |
| **Integer** | `int`, `long`, `short`, etc. | GreaterThan, AtLeast, LessThan, AtMost, Between, Positive, MultipleOf|
| **Floating-point** | `float`, `double`, `decimal`, `Half` | GreaterThan, AtLeast, LessThan, AtMost, Between, Positive, MultipleOf, IsFinite, IsNotNaN |
| **Dates** | `DateTime`, `DateOnly`, `DateTimeOffset` | OnOrBefore, OnOrAfter, Between |
| **TimeSpan** | `TimeSpan` | OnOrBefore, OnOrAfter, Around |
| **GUID** | `Guid` | NotEmpty, Version4, Version7 |
| **Enum** | `Enum` | IsDefined, OneOf, NotOneOf |
| **Predicate** | Any type | Must, MustNot (custom predicate-based validation) |

For detailed documentation of all available rules, see [Rules](../../docs/Rules.md).

---

## Customization

### Failure messages

In addition to the default (English) validation messages, you can provide custom messages for each rule. This allows you to tailor the feedback to your application's context.
```csharp
personResult
	.Validate(p => p.Age).GreaterThan(18, "Person must be an adult");
```

### Property names

The property name is automatically detected from the property expression, but you can also provide a custom property name if needed:
```csharp
personResult
	.Validate(p => p.Age, "PersonAge").GreaterThan(18, "Person must be an adult");
```

### Tolerances
For floating point validations a default tolerance is used:

| Type | Default Tolerance |
| --------- | ----------------- |
| `float` | 1e-6 |
| `double` | 1e-9 |
| `decimal` | 1e-9 |
| `Half` | 1e-2 |

This is useful for rounding errors when comparing floating point numbers. You can override the default tolerance by providing a custom value:
```csharp
personResult
	.Validate(p => p.Height).AtLeast(1.75d, tolerance: 0.01d);
```

---

## License

MIT License. See [LICENSE](../../LICENSE) for details.