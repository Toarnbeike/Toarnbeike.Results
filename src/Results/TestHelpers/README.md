# Toarnbeike.Results.TestHelpers

Assertion extensions for verifying `Toarnbeike.Results` in unit tests. 

This namespace provides fluent-style, exception-based assertions to validate the outcome of `Result` and `Result<T>` instances. 
It has no external dependencies and can be used with xUnit, NUnit, or MSTest.
Even though it is part of the main library, these extension methods are not intended for use in production logic.

---

## Features

- Assert that a Result or Result<T> is a failure
- Validate the failure's code and/or message
- Assert on specific Failure types
- Add custom messages or predicates for failure verification

---

## Available assertions 

| Method                                                | Description                                                                       |
|-------------------------------------------------------|-----------------------------------------------------------------------------------|
| `ShouldBeSuccess()`                         	        | Verifies that a Result is successful.                                             |
| `ShouldBeSuccess<T>()`                                | Verifies success and returns the inner value.                                     |
| `ShouldBeSuccessWithValue(expected)`	                | Verifies success and that the result has the expected value.                      |
| `ShouldBeSuccessThatSatisfiedPredicate(predicate)`    | Verifies success and that the result value satisfies the given predicate.         |
| `ShouldBeFailure()`                                   | Verifies that the result is a failure.                                            |
| `ShouldBeFailureWithCode(code)`                 	    | Verifies that the failure has the specified code.                                 |
| `ShouldBeFailureWithMessage(message)`                 | Verifies that the failure has the specified message.                              |
| `ShouldBeFailureWithCodeAndMessage(code, message)`    | Verifies that the failure has the specified code and message.                     |
| `ShouldBeFailureOfType<TFailure>()`                   | Verifies that the failure is of the specified type.                               |
| `ShouldBeFailureThatSatisfiesPredicate(predicate)`    | Verifies that the failure matches the given condition.                            |

---

## Usage

``` csharp
using Toarnbeike.Results;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestHelpers;;

public class MyTests
{
    [Fact]
    public void Should_Succeed_WithExpectedValue()
    {
        Result<string> result = GetConfigurationItem();

        result.ShouldBeSuccessWithValue("configItem1");
    }

    [Fact]
    public void Should_Fail_WithExpectedCode()
    {
        IResult result = SomeServiceCall();

        result.ShouldBeFailureWithCode("Unauthorized");
    }

    [Fact]
    public void Should_Fail_WithExpectedFailureType()
    {
        IResult result = AnotherServiceCall();

        var validation = result.ShouldBeFailureOfType<ValidationError>();
        validation.Failures.ShouldNotBeEmpty();
    }
}

```