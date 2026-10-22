using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class NumericTests
{
    private readonly Result<int> _result = 5;
    private readonly Result<int> _negativeResult = -1;
    private readonly int _min = 3;
    private readonly int _max = 10;

    [Test]
    public void GreaterThan_ShouldReturnSuccess_WhenValueIsGreaterThanMin()
    {
        var validationResult = _result.TestRuleFor("Value").GreaterThan(_min);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void GreaterThan_ShouldReturnFailure_WhenValueIsNotGreaterThanMin()
    {
        var validationResult = _result.TestRuleFor("Value").GreaterThan(5);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void GreaterThan_ShouldReturnFailureWithCustomMessage_WhenValueIsNotGreaterThanMin()
    {
        var validationResult = _result.TestRuleFor("Value").GreaterThan(5, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void LessThan_ShouldReturnSuccess_WhenValueIsLessThanMax()
    {
        var validationResult = _result.TestRuleFor("Value").LessThan(_max);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void LessThan_ShouldReturnFailure_WhenValueIsNotLessThanMax()
    {
        var validationResult = _result.TestRuleFor("Value").LessThan(5);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void LessThan_ShouldReturnFailureWithCustomMessage_WhenValueIsNotLessThanMax()
    {
        var validationResult = _result.TestRuleFor("Value").LessThan(5, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Positive_ShouldReturnSuccess_WhenValueIsPositive()
    {
        var validationResult = _result.TestRuleFor("Value").Positive();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Positive_ShouldReturnFailure_WhenValueIsNotPositive()
    {
        var validationResult = _negativeResult.TestRuleFor("Value").Positive();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Positive_ShouldReturnFailureWithCustomMessage_WhenValueIsNotPositive()
    {
        var validationResult = _negativeResult.TestRuleFor("Value").Positive("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
