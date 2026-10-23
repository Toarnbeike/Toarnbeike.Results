using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Validation;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class AsyncPredicateTests
{
    [Test]
    public async Task MustAsync_ShouldReturnSuccess_WhenPredicateIsTrue()
    {
        Result<int> result = 5;
        var validationResult = await result.Validate(x => x).MustAsync(value => Task.FromResult(value > 0), "Must be positive");
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public async Task MustAsync_ShouldReturnFailure_WithCustomMessage_WhenPredicateIsFalse()
    {
        Result<int> result = -5;
        var validationResult = await result.Validate(x => x).MustAsync(value => Task.FromResult(value > 0), "Must be positive");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("x");
        failure.ValidationMessage.ShouldBe("Must be positive");
    }

    [Test]
    public async Task MustNotAsync_ShouldReturnSuccess_WhenPredicateIsFalse()
    {
        Result<int> result = -5;
        var validationResult = await result.Validate(x => x).MustNotAsync(value => Task.FromResult(value > 0), "Must not be positive");
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public async Task MustNotAsync_ShouldReturnFailure_WhenPredicateIsTrue()
    {
        Result<int> result = 5;
        var validationResult = await result.Ensure(x => x).MustNotAsync(value => Task.FromResult(value > 0), "Must not be positive");
        var failure = validationResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("x");
        failure.Message.ShouldBe("Must not be positive");
    }

    [Test]
    public async Task MustAsync_ShouldPreserveAnExistingFailure()
    {
        Result<int> result = new TestFailure("Original failure");
        var predicateCalled = false;
        var validationResult = await result.Validate(x => x).MustAsync(value =>
        {
            predicateCalled = true;
            return Task.FromResult(value > 0);
        }, "Must be positive");

        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original failure");
        predicateCalled.ShouldBeFalse();
    }

    [Test]
    public async Task MustAsync_ShouldIncludeFirstFailingIndex_ForAllCollectionTarget()
    {
        Result<List<int>> result = Result.Success(new List<int> { 1, 2, 3 });
        var validationResult = await result.ValidateCollection(values => values, "Values")
            .All()
            .MustAsync(value => Task.FromResult(value > 1), "Must be greater than one");

        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("Values[0]");
    }

    [Test]
    public async Task MustAsync_ShouldShortCircuitAnyCollectionTarget()
    {
        Result<List<int>> result = Result.Success(new List<int> { 1, 2, 3 });
        var calls = 0;
        var validationResult = await result.ValidateCollection(values => values)
            .Any()
            .MustAsync(value =>
            {
                calls++;
                return Task.FromResult(value == 2);
            }, "At least one value must equal two");

        validationResult.ShouldBeSuccess();
        calls.ShouldBe(2);
    }
}
