using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class EnumExtensionTests
{
    private enum TestEnum
    {
        Value1,
        Value2,
        Value3,
    };

    [Test]
    public void IsDefined_Should_ReturnFormattedFailure()
    {
        var value = (TestEnum)100;
        var result = value.IsDefined();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: value,
            expectedMessage: "'value' must be a defined value of 'TestEnum', but is not (value '100' is unknown).");
    }

    [Test]
    public void OneOf_Should_ReturnFormattedFailure()
    {
        var value = TestEnum.Value1;
        TestEnum[] validValues = [TestEnum.Value2, TestEnum.Value3];
        var result = value.OneOf(validValues);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: value,
            expectedMessage: "'value' must be one of the following values: [Value2, Value3], but is Value1."
        );
    }

    [Test]
    public void NotOneOf_Should_ReturnFormattedFailure()
    {
        var value = TestEnum.Value1;
        TestEnum[] invalidValues = [TestEnum.Value1, TestEnum.Value3];
        var result = value.NotOneOf(invalidValues);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: value,
            expectedMessage: "'value' must not be one of the following values: [Value1, Value3], but is Value1."
        );
    }
}