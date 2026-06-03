using Toarnbeike.Results.Guards.Extensions;

namespace Toarnbeike.Results.Guards.Tests.Extensions;

public class TimeSpanExtensionTests
{
    private readonly TimeSpan _value = TimeSpan.FromSeconds(10);
    private readonly TimeSpan _negative = TimeSpan.FromSeconds(-10);
    private readonly TimeSpan _shorter = TimeSpan.FromSeconds(5);
    private readonly TimeSpan _longer = TimeSpan.FromSeconds(15);

    [Test]
    public void AtLeast_Should_ReturnFormattedFailure()
    {
        var result = _value.AtLeast(_longer);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at least 00:00:15, but is 00:00:10."
        );
    }

    [Test]
    public void AtMost_Should_ReturnFormattedFailure()
    {
        var result = _value.AtMost(_shorter);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at most 00:00:05, but is 00:00:10."
        );
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure()
    {
        var result = _value.Between(TimeSpan.Zero, _shorter);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be between 00:00:00 and 00:00:05, but is 00:00:10."
        );
    }

    [Test]
    public void Around_Should_ReturnFormattedFailure()
    {
        var result = _value.Around(_shorter, TimeSpan.FromSeconds(2));
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be around 00:00:05 ± 00:00:02, but is 00:00:10."
        );
    }

    [Test]
    public void AtLeastZero_Should_ReturnFormattedFailure()
    {
        var result = _negative.AtLeastZero();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _negative,
            expectedMessage: "'_negative' must be at least zero, but is -00:00:10."
        );
    }

    [Test]
    public void AtMostZero_Should_ReturnFormattedFailure()
    {
        var result = _value.AtMostZero();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at most zero, but is 00:00:10."
        );
    }
}