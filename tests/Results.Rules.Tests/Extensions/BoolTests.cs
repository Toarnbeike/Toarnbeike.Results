using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class BoolTests
{
    private readonly Result<bool> _trueResult = true;
    private readonly Result<bool> _falseResult = false;

    [Test]
    public void IsTrue_ShouldReturnSuccess_WhenValueIsTrue()
    {
        var result = _trueResult.TestRuleFor("boolean").IsTrue();
        result.ShouldBeSuccess();
    }

    [Test]
    public void IsTrue_ShouldReturnFailure_WhenValueIsFalse()
    {
        var result = _falseResult.TestRuleFor("boolean").IsTrue();
        _ = result.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void IsTrue_ShouldReturnFailureWithCustomMessage_WhenValueIsFalse()
    {
        var result = _falseResult.TestRuleFor("boolean").IsTrue("Custom failure message");
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void IsFalse_ShouldReturnSuccess_WhenValueIsFalse()
    {
        var result = _falseResult.TestRuleFor("boolean").IsFalse();
        result.ShouldBeSuccess();
    }

    [Test]
    public void IsFalse_ShouldReturnFailure_WhenValueIsTrue()
    {
        var result = _trueResult.TestRuleFor("boolean").IsFalse();
        _ = result.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void IsFalse_ShouldReturnFailureWithCustomMessage_WhenValueIsTrue()
    {
        var result = _trueResult.TestRuleFor("boolean").IsFalse("Custom failure message");
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
