using Toarnbeike.Results.Ensure.PrimitiveExtensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.PrimitiveExtensions;

public class EnumExtensionTests
{
    private enum TestEnum
    {
        Success = 0,
    }

    [Test]
    public void IsDefined_Should_ReturnFailure_WhenNotDefined()
    {
        var value = (TestEnum)1;
        var result = value.IsDefined();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("IsDefined");
        failure.Expression.ShouldBe(nameof(value));
        failure.Constraint.ShouldBeNull();
        failure.AttemptedValue.ShouldBe(value);
        failure.Message.ShouldBe($"'value' is not a valid value.");
    }

    [Test]
    public void IsDefined_Should_ReturnSuccess_WhenValid()
    {
        var value = TestEnum.Success;
        var result = value.IsDefined();
        result.ShouldBeSuccess().ShouldBe(value);
    }
}