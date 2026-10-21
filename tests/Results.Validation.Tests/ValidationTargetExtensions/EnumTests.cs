using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

public class EnumTests
{
    [Test]
    public void IsDefined_ShouldReturnSuccess_WhenEnumValueIsDefined()
    {
        Result<DayOfWeek> result = DayOfWeek.Monday;
        var validationResult = result.Validate("DayOfWeek").IsDefined();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void IsDefined_ShouldReturnFailure_WhenEnumValueIsNotDefined()
    {
        Result<DayOfWeek> result = (DayOfWeek)100; // Invalid enum value
        var validationResult = result.Validate("DayOfWeek").IsDefined();
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void IsDefined_ShouldReturnFailureWithCustomMessage_WhenEnumValueIsNotDefined()
    {
        Result<DayOfWeek> result = (DayOfWeek)100; // Invalid enum value
        var validationResult = result.Validate("DayOfWeek").IsDefined("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void OneOf_ShouldReturnSuccess_WhenEnumValueIsOneOfSpecifiedValues()
    {
        Result<DayOfWeek> result = DayOfWeek.Monday;
        var validationResult = result.Validate("DayOfWeek").OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OneOf_ShouldReturnFailure_WhenEnumValueIsNotOneOfSpecifiedValues()
    {
        Result<DayOfWeek> result = DayOfWeek.Wednesday;
        var validationResult = result.Validate("DayOfWeek").OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void OneOf_ShouldReturnFailureWithCustomMessage_WhenEnumValueIsNotOneOfSpecifiedValues()
    {
        Result<DayOfWeek> result = DayOfWeek.Wednesday;
        var validationResult = result.Validate("DayOfWeek").OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void NotOneOf_ShouldReturnSuccess_WhenEnumValueIsNotOneOfSpecifiedValues()
    {
        Result<DayOfWeek> result = DayOfWeek.Wednesday;
        var validationResult = result.Validate("DayOfWeek").NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotOneOf_ShouldReturnFailure_WhenEnumValueIsOneOfSpecifiedValues()
    {
        Result<DayOfWeek> result = DayOfWeek.Monday;
        var validationResult = result.Validate("DayOfWeek").NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void NotOneOf_ShouldReturnFailureWithCustomMessage_WhenEnumValueIsOneOfSpecifiedValues()
    {
        Result<DayOfWeek> result = DayOfWeek.Monday;
        var validationResult = result.Validate("DayOfWeek").NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }
}
