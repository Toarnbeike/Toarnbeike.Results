using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

public class PredicateTests
{
    [Test]
    public void Must_ShouldReturnSuccess_WhenPredicateIsTrue()
    {
        Result<int> result = 5;
        var validationResult = result.Validate("Value").Must(x => x > 0, "Value must be greater than 0");
        validationResult.ShouldBeSuccess();
    }
    [Test]
    public void Must_ShouldReturnFailure_WhenPredicateIsFalse()
    {
        Result<int> result = -5;
        var validationResult = result.Validate("Value").Must(x => x > 0, "Value must be greater than 0");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must be greater than 0");
    }

    [Test]
    public void MustNot_ShouldReturnSuccess_WhenPredicateIsFalse()
    {
        Result<int> result = -5;
        var validationResult = result.Validate("Value").MustNot(x => x > 0, "Value must not be greater than 0");
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void MustNot_ShouldReturnFailure_WhenPredicateIsTrue()
    {
        Result<int> result = 5;
        var validationResult = result.Validate("Value").MustNot(x => x > 0, "Value must not be greater than 0");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Value must not be greater than 0");
    }
}
