using FluentValidation;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.FluentValidation.Tests;

/// <summary>
/// Tests for <see cref="ValidateExtensions"/>.
/// </summary>
public class ValidateExtensionTests
{
    private sealed class Person
    {
        public string Name { get; init; } = string.Empty;
        public int Age { get; init; }
    }

    private sealed class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Age).GreaterThanOrEqualTo(18);
        }
    }

    private sealed class AsyncPersonValidator : AbstractValidator<Person>
    {
        public AsyncPersonValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Age).MustAsync(async (age, _) =>
            {
                await Task.Yield();
                return age >= 18;
            });
        }
    }

    [Fact]
    public void Validate_Should_ReturnOriginalResult_IfAlreadyFailed()
    {
        var result = Result<Person>.Failure(new Failure("test", "already failed"));

        var validated = result.Validate(new PersonValidator());

        validated.ShouldBeFailureWithCodeAndMessage("test", "already failed");
    }

    [Fact]
    public void Validate_Should_ReturnValidationFailures_WhenValidationFails()
    {
        var person = new Person { Name = "", Age = 15 };
        var result = Result.Success(person);

        var validated = result.Validate(new PersonValidator());

        var failures = validated.ShouldBeFailureOfType<ValidationFailures>();

        failures.GetFailuresFor("Name").Count().ShouldBe(1);
        failures.GetFailuresFor("Age").Count().ShouldBe(1);
    }

    [Fact]
    public void Validate_Should_ReturnOriginalResult_WhenValidationSucceeds()
    {
        var person = new Person { Name = "Alice", Age = 30 };
        var result = Result.Success(person);

        var validated = result.Validate(new PersonValidator());

        validated.ShouldBeSuccessWithValue(person);
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnOriginalResult_IfAlreadyFailed()
    {
        var result = Result<Person>.Failure(new Failure("test", "already failed"));

        var validated = await result.ValidateAsync(new PersonValidator());

        validated.ShouldBeFailureWithCodeAndMessage("test", "already failed");
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnValidationFailures_WhenValidationFails()
    {
        var person = new Person { Name = "", Age = 15 };
        var result = Result.Success(person);

        var validated = await result.ValidateAsync(new PersonValidator());

        var failures = validated.ShouldBeFailureOfType<ValidationFailures>();

        failures.GetFailuresFor("Name").Count().ShouldBe(1);
        failures.GetFailuresFor("Age").Count().ShouldBe(1);
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnOriginalResult_WhenValidationSucceeds()
    {
        var person = new Person { Name = "Alice", Age = 30 };
        var result = Result.Success(person);

        var validated = await result.ValidateAsync(new PersonValidator());

        validated.ShouldBeSuccessWithValue(person);
    }

    [Fact]
    public async Task Validate_Should_ReturnOriginalResultTask_IfAlreadyFailed()
    {
        var result = Task.FromResult(Result<Person>.Failure(new Failure("test", "already failed")));

        var validated = await result.Validate(new PersonValidator());

        validated.ShouldBeFailureWithCodeAndMessage("test", "already failed");
    }

    [Fact]
    public async Task Validate_Should_ReturnValidationFailures_WhenValidationOfTaskFails()
    {
        var person = new Person { Name = "", Age = 15 };
        var result = Task.FromResult(Result.Success(person));

        var validated = await result.Validate(new PersonValidator());

        var failures = validated.ShouldBeFailureOfType<ValidationFailures>();

        failures.GetFailuresFor("Name").Count().ShouldBe(1);
        failures.GetFailuresFor("Age").Count().ShouldBe(1);
    }

    [Fact]
    public async Task Validate_Should_ReturnOriginalResult_WhenValidationOfTaskSucceeds()
    {
        var person = new Person { Name = "Alice", Age = 30 };
        var result = Task.FromResult(Result.Success(person));

        var validated = await result.Validate(new PersonValidator());

        validated.ShouldBeSuccessWithValue(person);
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnOriginalResultTask_IfAlreadyFailed()
    {
        var result = Task.FromResult(Result<Person>.Failure(new Failure("test", "already failed")));

        var validated = await result.ValidateAsync(new PersonValidator());

        validated.ShouldBeFailureWithCodeAndMessage("test", "already failed");
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnValidationFailures_WhenValidationOfTaskFails()
    {
        var person = new Person { Name = "", Age = 15 };
        var result = Task.FromResult(Result.Success(person));

        var validated = await result.ValidateAsync(new PersonValidator());

        var failures = validated.ShouldBeFailureOfType<ValidationFailures>();

        failures.GetFailuresFor("Name").Count().ShouldBe(1);
        failures.GetFailuresFor("Age").Count().ShouldBe(1);
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnOriginalResult_WhenValidationOfTaskSucceeds()
    {
        var person = new Person { Name = "Alice", Age = 30 };
        var result = Task.FromResult(Result.Success(person));

        var validated = await result.ValidateAsync(new PersonValidator());

        validated.ShouldBeSuccessWithValue(person);
    }
}
