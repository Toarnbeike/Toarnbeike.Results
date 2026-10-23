using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class PredicateTests
{
    [Test]
    public void Must_ShouldReturnSuccess_WhenPredicateIsTrue()
    {
        Result<int> result = 5;
        var validationResult = result.TestRuleFor("Value").Must(x => x > 0, "Value must be greater than 0");
        validationResult.ShouldBeSuccess();
    }
    [Test]
    public void Must_ShouldReturnFailure_WhenPredicateIsFalse()
    {
        Result<int> result = -5;
        var validationResult = result.TestRuleFor("Value").Must(x => x > 0, "Value must be greater than 0");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Value must be greater than 0");
    }

    [Test]
    public void MustNot_ShouldReturnSuccess_WhenPredicateIsFalse()
    {
        Result<int> result = -5;
        var validationResult = result.TestRuleFor("Value").MustNot(x => x > 0, "Value must not be greater than 0");
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void MustNot_ShouldReturnFailure_WhenPredicateIsTrue()
    {
        Result<int> result = 5;
        var validationResult = result.TestRuleFor("Value").MustNot(x => x > 0, "Value must not be greater than 0");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Value must not be greater than 0");
    }
}
