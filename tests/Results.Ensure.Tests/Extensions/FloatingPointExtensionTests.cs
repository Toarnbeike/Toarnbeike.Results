using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class FloatingPointExtensionTests : InvariantCultureTestBase
{
    private readonly string _customFailureMessage = "Custom message";
    private readonly float _value = 5.550f;

    [Test]
    public void WholeNumber_Should_ReturnFormattedFailure()
    {
        var result = _value.WholeNumber();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("WholeNumber");
        failure.Message.ShouldBe($"'{nameof(_value)}' must be a multiple of 1, but is {_value}.");
    }

    [Test]
    public void WholeNumber_Should_ReturnCustomMessageFailure()
    {
        var result = _value.WholeNumber(message: _customFailureMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customFailureMessage);
    }

    [Test]
    public void MaxDecimalPlaces_Should_ReturnFormattedFailure()
    {
        var result = _value.MaxDecimalPlaces(1);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("MaxDecimalPlaces");
        failure.Message.ShouldBe($"'{nameof(_value)}' must be a multiple of 0.1, but is {_value}.");
    }

    [Test]
    public void MaxDecimalPlaces_Should_ReturnCustomMessageFailure()
    {
        var result = _value.MaxDecimalPlaces(1, message: _customFailureMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customFailureMessage);
    }

    [Test]
    public void MaxDecimalPlaces_Should_ReturnSuccess_WithTrailingZeros()
    {
        var result = _value.MaxDecimalPlaces(2);
        result.ShouldBeSuccess().ShouldBe(_value);
    }

    [Test]
    public void MaxDecimalPlaces_Should_Throw_WhenPowerIsNegative()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => _value.MaxDecimalPlaces(-1));
    }

    [Test]
    public void Power10_Should_RaiseCorrectly_ForDifferentTypes()
    {
        FloatingPointExtensions.Power10<float>(3).ShouldBe(1000f);
        FloatingPointExtensions.Power10<double>(5).ShouldBe(100000d);
        FloatingPointExtensions.Power10<decimal>(2).ShouldBe(100m);
        FloatingPointExtensions.Power10<Half>(1).ShouldBe(Half.Parse("10"));
        FloatingPointExtensions.Power10<float>(0).ShouldBe(1f);
    }
}