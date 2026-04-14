using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class EnsureEnumTests
{
    private enum TestEnum
    {
        Success = 0,
    }

    [Test]
    public void IsDefined_Should_ReturnFailure_WhenNotDefined()
    {
        var value = (TestEnum)1;
        var result = Ensure.IsDefined(value);
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
        var result = Ensure.IsDefined(value);
        result.ShouldBeSuccess().ShouldBe(value);
    }
}