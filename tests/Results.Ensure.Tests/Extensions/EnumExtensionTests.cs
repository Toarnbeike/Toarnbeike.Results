using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class EnumExtensionTests
{
    private enum TestEnum { }

    [Test]
    public void IsDefined_Should_ReturnFormattedFailure()
    {
        var value = (TestEnum)1;
        var result = value.IsDefined();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("IsDefined");
        failure.Message.ShouldBe("'value' (1) is not a defined TestEnum.");
    }

    [Test]
    public void IsDefined_Should_ReturnCustomMessageFailure()
    {
        var value = (TestEnum)1;
        var result = value.IsDefined("Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}