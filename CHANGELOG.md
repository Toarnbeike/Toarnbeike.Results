# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)
and this project adheres to [Semantic Versioning](https://semver.org/).

## [1.1.1] - 2026-10-4

### Changed
- Introduced `Toarnbeike.Results.Abstractions` for source generator abstractions
- First start with cleaning the Readme file.

## [1.1.0] - 2026-10-4

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