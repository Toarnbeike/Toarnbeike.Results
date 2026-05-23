using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class EnumRuleTests
{
    private enum TestEnum
    {
        Value1,
        Value2,
        Value3,
    };

    private readonly TestEnum _undefined = (TestEnum)100;
    private readonly TestEnum _value = TestEnum.Value1;

    [Test]
    public void IsDefined()
    {
        var result = Result.Ensure().That(_undefined).IsDefined();
        result.AssertFailure(
            expectedAttemptedValue: _undefined,
            expectedArgumentName: "_undefined",
            expectedGuardName: "Enum.IsDefined",
            ("EnumType", typeof(TestEnum))
        );
    }

    [Test]
    public void OneOf()
    {
        TestEnum[] validValues = [TestEnum.Value2, TestEnum.Value3];
        var result = Result.Ensure().That(_value).OneOf(validValues);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Enum.OneOf",
            ("ValidValues", validValues.Select(v => v.ToString()))
        );
    }

    [Test]
    public void NotOneOf()
    {
        TestEnum[] invalidValues = [TestEnum.Value1, TestEnum.Value3];
        var result = Result.Ensure().That(_value).NotOneOf(invalidValues);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Enum.NotOneOf",
            ("InvalidValues", invalidValues.Select(v => v.ToString()))
        );
    }
}