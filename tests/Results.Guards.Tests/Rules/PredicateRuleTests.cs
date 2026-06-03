using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Tests.Rules;

public class PredicateRuleTests
{
    private readonly int _value = 1;
    private readonly int _negativeValue = -1;

    private readonly Func<int, bool> _negative = x => x < 0;
    private readonly Func<int, Task<bool>> _negativeAsync = x => Task.FromResult(x < 0);

    [Test]
    public void Satisfies_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Satisfies(_negative);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Predicate.Satisfies",
            ("Predicate", _negative)
        );
    }

    [Test]
    public void NotSatisfies_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_negativeValue).NotSatisfies(_negative);
        result.AssertFailure(
            expectedAttemptedValue: _negativeValue,
            expectedArgumentName: "_negativeValue",
            expectedGuardName: "Predicate.NotSatisfies",
            ("Predicate", _negative)
        );
    }

    [Test]
    public async Task SatisfiesAsync_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = await Result.Ensure().That(_value).SatisfiesAsync(_negativeAsync);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Predicate.Satisfies",
            ("Predicate", _negativeAsync)
        );
    }

    [Test]
    public async Task NotSatisfiesAsync_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = await Result.Ensure().That(_negativeValue).NotSatisfiesAsync(_negativeAsync);
        result.AssertFailure(
            expectedAttemptedValue: _negativeValue,
            expectedArgumentName: "_negativeValue",
            expectedGuardName: "Predicate.NotSatisfies",
            ("Predicate", _negativeAsync)
        );
    }
}