![CI](https://github.com/Toarnbeike/Toarnbeike.Results/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](../../LICENSE)

# Toarnbeike.Results.Validation

`Toarnbeike.Results.Validation` adds reusable object validators to **Toarnbeike.Results**. Define rules for a type with `Validator<T>` and collect all validation failures into a `ValidationFailureSummary`.

Validators use the type rules from `Toarnbeike.Results.Rules` and integrate with `Result<T>` through `ValidateUsing`.

---

## Contents

1. [Quick start](#quick-start)
1. [Validator<T>](#validatort)
1. [Collection and nested validation](#collection-and-nested-validation)
1. [Failures and customization](#failures-and-customization)
1. [Related packages](#related-packages)

---

## Quick start

Create a validator by deriving from `Validator<T>` and registering rules in `RegisterRules`:

```csharp
using Toarnbeike.Results;
using Toarnbeike.Results.Validation;
using Toarnbeike.Results.Validation.Extensions;

public sealed record Customer(string Name, int Age, IEnumerable<string> Tags);

public sealed class CustomerValidator : Validator<Customer>
{
	protected override void RegisterRules(ValidationRuleCollection<Customer> rules)
	{
		rules.That(customer => customer.Name).NotWhiteSpace();
		rules.That(customer => customer.Age).AtLeast(18);
		rules.ThatCollection(customer => customer.Tags).All().NotWhiteSpace();
	}
}

Result<Customer> customerResult = GetCustomer();
var validated = customerResult.ValidateUsing(new CustomerValidator());
```

---

## Validator<T>

`Validator<T>` defines reusable validation rules for a type. Override `RegisterRules` to select properties, collections, and nested values, then add the shared type rules from `Toarnbeike.Results.Validation.Extensions`.

Calling `validator.Validate(value)` returns the original value on success or a `ValidationFailureSummary` when any rules fail. Calling `result.ValidateUsing(validator)` validates a successful `Result<T>` and leaves an incoming failure unchanged.

Unlike the short-circuiting `Validate` extensions in `Toarnbeike.Results.Rules`, a validator evaluates all registered rules and aggregates their failures.

---

## Collection and nested validation

Use `ThatCollection(selector)` to apply rules to the collection itself. Call `All()` to apply the following rule to every item; each failing item contributes a failure under an indexed path such as `Tags[1]`. `All()` evaluates every item, and an empty collection passes.

Call `Any()` when at least one item must pass the following rule. Evaluation stops when an item passes. If no item passes, including when the collection is empty, the failure is reported against the collection property.

Nested validators can be applied to a property or to every item in a collection:

```csharp
rules.That(customer => customer.Address).ValidateUsing(new AddressValidator());
rules.ThatCollection(customer => customer.Lines).All().ValidateUsing(new LineValidator());
```

Nested failure paths include their parent path, for example `Address.City` or `Lines[0].Quantity`.

---

## Failures and customization

Property names are inferred from selector expressions. Supply an explicit property name when needed, and pass a custom message to an individual rule:

```csharp
rules.That(customer => customer.Age, "CustomerAge")
	.AtLeast(18, "Customer must be an adult.");
```

`ValidationFailureSummary` groups all failures by property name. Built-in type rules and generated messages are shared with `Toarnbeike.Results.Rules`; see the [Rules README](../Results.Rules/README.md) and [detailed rule reference](../../docs/Rules.md) for available rules and `RuleContext` settings.

---

## Related packages

- `Toarnbeike.Results.Rules` provides result-level `Ensure` and `Validate` extensions that short-circuit on failure.
- `Toarnbeike.Results` provides the `Result<T>` type and core result operations.

---

## License

MIT License. See [LICENSE](../../LICENSE) for details.