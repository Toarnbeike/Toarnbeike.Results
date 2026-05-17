using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class TimeSpanExtensionTests : InvariantCultureTestBase
{
    private readonly TimeSpan _value = TimeSpan.FromSeconds(1);
    private readonly TimeSpan _negative = TimeSpan.FromSeconds(-1);
    private readonly TimeSpan _shorter = TimeSpan.FromSeconds(0.5);
    private readonly TimeSpan _longer = TimeSpan.FromSeconds(1.5);

    private readonly string _customMessage = "CustomFailureMessage";

    [Test]
    public void AtLeast_Should_ReturnFormattedFailure()
    {
        var result = _value.AtLeast(_longer);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtLeast");
        failure.Message.ShouldBe($"'{nameof(_value)}' must be at least 00:00:01.5000000, but is 00:00:01.");
    }

    [Test]
    public void AtLeast_Should_ReturnCustomMessageFailure()
    {
        var result = _value.AtLeast(_longer, _customMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customMessage);
    }

    [Test]
    public void AtMost_Should_ReturnFormattedFailure()
    {
        var result = _value.AtMost(_shorter);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtMost");
        failure.Message.ShouldBe($"'{nameof(_value)}' must be at most 00:00:00.5000000, but is 00:00:01.");
    }

    [Test]
    public void AtMost_Should_ReturnCustomMessageFailure()
    {
        var result = _value.AtMost(_shorter, _customMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customMessage);
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure_WhenValueIsTooShort()
    {
        var result = _value.Between(_longer, _longer);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtLeast");
        failure.Message.ShouldBe($"'{nameof(_value)}' must be at least 00:00:01.5000000, but is 00:00:01.");
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure_WhenValueIsTooLong()
    {
        var result = _value.Between(_shorter, _shorter);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtMost");
        failure.Message.ShouldBe($"'{nameof(_value)}' must be at most 00:00:00.5000000, but is 00:00:01.");
    }

    [Test]
    public void Between_Should_ReturnCustomMessageFailure()
    {
        var result = _value.Between(_shorter, _shorter, _customMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customMessage);
    }

    [Test]
    public void Positive_Should_ReturnFormattedFailure()
    {
        var result = _negative.Positive();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("AtLeast");
        failure.Message.ShouldBe($"'{nameof(_negative)}' must be at least 00:00:00, but is -00:00:01.");
    }

    [Test]
    public void Positive_Should_ReturnCustomMessageFailure()
    {
        var result = _negative.Positive(_customMessage);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe(_customMessage);
    }
}