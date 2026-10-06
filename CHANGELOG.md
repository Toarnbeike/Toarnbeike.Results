# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)
and this project adheres to [Semantic Versioning](https://semver.org/).

## [3.0.0-alpha.3] - 2026-10-06

### Depricated
- `SimpleFailure` - will be removed in 3.0.0, use `Failure` instead.

### Added
- `DomainFalure` - a new failure type for domain validation, allowing for more specific failure types and better integration with the `Ensure` library.
- `TestFailure` - a new failure type for testing, allowing for more specific failure types.

### Changed
- `Failure` - is no longer abstract, and can be used directly for simple failures. Replaces `SimpleFailure` which was just a concrete implementation of `Failure`.

## [3.0.0-alpha.1] - 2026-10-05

### Deprecated
- `Toarnbeike.Results.MinimalApi` - will not be maintained anymore, not part of the 3.0.0 release.
- `Toarnbeike.Results.FluentValidation` - will not be maintained anymore, not part of the 3.0.0 release. Use `Toarnbeike.Results.Validation` instead.

### Removed
- `Ensure`: primitive guard extensions are removed, Result.Ensure() has a new syntax.

### Added
- `Ensure`: new syntax for domain validation:
	- `Result.Ensure(Func<TValue>).[Rule]()` - for inline guard checks on `Result`
	- `Result<T>.Ensure(Func<T, TProperty>).[Rule]()` - for inline guard checks on `Result<T>`
	- Support for EnsureAll, EnsureAny, both on `Result `and `Result<T>`
- Common base between `Ensure` and `Validation` libraries, allowing for shared rules and extensibility.
- `Core`: Bind extensions between `Result` and `Result` for consistency.

### Upcoming
- Overhaul (again) of the failure system, to allow for more flexibility and better integration with other libraries.
- Review of available extensions, to ensure consistency and discoverability across the library.

## [2.2.1] - 2026-10-04

### Fixes
- Fixed that `FloatingPointRules` did not accept `decimal` values, since these don't implement `IFloatingPointIeee754<TSelf>`. Now `decimal` is explicitly supported.
- Fixed a bug in Around Timespan validation rule where the tolerance was not being applied correctly when close to TimeSpan.MinValue or TimeSpan.MaxValue.
- Fixed a bug in property name extraction where lambdas without spaces where not properly detected.
- Fixed a bug when provided tolerances for floating point validations where not finite.
- Fixed a few bugs in Readme examples where the code snippets did not compile correctly.
- Fixed a bug in Validation lazy initialization where the rules could be materialized twice.

### Performance
- Improved performance of Collection rules not implementing ICollection by using partial enumeration where possible.

### Tooling
- Textual improvements in multiple test descriptions.

## [2.2.0] - 2026-10-04

### Added
- Added `Toarnbeike.Results.Validation` library for application validation.

### Tooling
- Upgraded nuget depencencies to latest versions (no breaking changes)

## [2.1.0] - 2026-5-14

### Removed
- All obsolete methods from 2.0.0

### Tooling
- Improved documentation
- Added missing tests

## [2.0.0] - 2026-4-14

Major rework of the Failure system. 
- Failure is abstract, contains a FailureCategory enum.
- SimpleFailure is introduced to represent failures when specific failures are not applicable.
- AggregateFailure and ValidationFailures have become atomic summaries.
- For a full description of the new failure system, see the [failure docs](docs/Failures.md).

Introduced `Toarnbeike.Results.Ensure` library for domain validation.
See [Ensure ReadMe](src/Results.Ensure/README.md)

### Added
- Extensions.BindTap - Chains a result-producing operation without changing the original value
- Extensions.Combine - Combines two successful results into a new value
- Extensions.CombineBind - Combines two successful results into a new result
- Async overloads for assertions, such as `ShouldBeSuccessAsync()` and `ShouldBeFailureOfTypeAsync<>()`

### Obsolete
- `Result<TValue>`.`TryGetValue(out var value, out var failure)` - made internal to force using the `TryGetValue(out var value)` or `Match()` methods instead, which are more explicit and less error-prone.
- Extensions.Check - Improves discoverability of the library. Use `Bind` instead
- Extensions.TapAlways - Improves discoverability in pipelines. Use `Tap()` followed by `TapFailure()` to achieve the same goal
- Extensions.Verify - Renamed to `BindTap` to improve consistency
- Extensions.VerifyWhen - To improve discoverability. Use `BindTap` or a specific method instead.
- Extensions.Zip - To discurrage tuple use. Use the newly introduced `Combine` extensions to directly map two results, or use a method to avoid having to use a tuple
- Extensions.Unsafe.GetFailureOrThrow() - To improve discoverability of the library. Use `TryGetFailure(out var failure)` instead.
- Results.TestHelpers - Namespace is renamed to Results.TestExtensions for consistency with other `Toarnbeike` libraries.
- Results.TestHelpers.ShouldBeSuccessWithValue() - Removes API surface duplication. Use `ShouldBeSuccess().ShouldBe(expected)` instead.
- Results.TestHelpers.ShouldBeSuccessThatSatisfiesPredicate() - Removes API surface duplication. Do an assertion on the `TValue` direct after `ShouldBeSuccess()`.
- Results.TestHelpers.ShouldBeFailureWithCode() - Removes API surface duplication. Use `ShouldBeFailure().Code.ShouldBe(expected)`.
- Results.TestHelpers.ShouldBeFailureWithMessage() - Removes API surface duplication. Use `ShouldBeFailure().Message.ShouldBe(expected)`.
- Results.TestHelpers.ShouldBeFailureWithCodeAndMessage() - Removes API surface duplication. Use `ShouldBeFailure()` followed by assertions on the `Failure`.
- Results.TestHelpers.ShouldBeFailureThatSatisfiesPredicate() - Removes API surface duplication. Use `ShouldBeFailure()` followed by assertions on the `Failure`.

### Changed
- Refactored code to use extension blocks for better organization and readability.
- Refactored collections into logical groups without modifying the API surface.
- Greatly improved readability of the documentation

### Tooling
- Introduced DebuggerDisplay on `Failure`, `Result` and `Result<T>` for improved debugging.

## [1.1.4] - 2026-4-12

### Added
- Added docs for specific pages, removing the generic README.md files from within the source.
- Added a future document highlighting the future of the package and the roadmap for the next versions.

### Changed
- Improved documentation of the main package, bringing it more in line with the other `Toarnbeike` packages.

## [1.1.3] - 2026-4-11

### Added
- Dedicated readme for `Toarnbeike.Results.Abstractions`.


## [1.1.2] - 2026-4-11

### Added
- Added Results.Abstractions.Tests, multitargeting .net472 (for netStandard2.0) and net10 for testing the abstractions.

## [1.1.1] - 2026-4-10

### Changed
- Introduced `Toarnbeike.Results.Abstractions` for source generator abstractions
- First start with cleaning the Readme file.

## [1.1.0] - 2026-4-10

### Added

### Changed
- Updated to net10

### Deprecated
- Results.Optional - will be moved to Toarnbeike.Functional integration package.
- Results.Messaging - will be moved to Toarnbeike.AppFramework.Dispatch or Toarnbeike.Dispatch

### Tooling
- Added changelog
- Start using Directory.Packages.props for Central package management
 
---

## [1.0.2] - 2025-11-04

### Added
- Overloads for `Zip()` that allow naming the tuple elements

---

## [1.0.1] - 2025-10-27

### Added
- Added TryValueAsync methods for ValueTask and ValueTask<T> 

---

## [1.0.0] - 2025-08-28

### Added
- Initial release of `Toarnbeike.Results`
- Explicit error handling: Avoid exceptions for control flow; return meaningful failures instead.
- Fluent extension methods: Compose operations with `Bind`, `Map`, `Match`, `Tap`, and more.
- Strongly typed generic results: Preserve either a success value or failure details in one type-safe container.
- Rich validation support: Aggregate validation failures with rich property-level details.
- Seamless integration: All methods support asynchronous pipelines.
- Functional programming inspired: Inspired by FP principles for predictable, readable, and maintainable error handling.