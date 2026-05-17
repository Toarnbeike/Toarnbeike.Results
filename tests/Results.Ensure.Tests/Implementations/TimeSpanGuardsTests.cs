using Toarnbeike.Results.Ensure.Implementations_Obsolete;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class TimeSpanGuardsTests
{
    private readonly TimeSpan _value = TimeSpan.FromSeconds(1);
    private readonly TimeSpan _shorter = TimeSpan.FromSeconds(0.5);
    private readonly TimeSpan _longer = TimeSpan.FromSeconds(1.5);

    [Test]
    public void AtLeast_Should_ReturnFailure_WhenShorter()
    {
        var result = TimeSpanGuards.AtLeast(_value, _longer);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_longer);
    }

    [Test]
    public void AtLeast_Should_ReturnSuccess_WhenEqual()
    {
        var result = TimeSpanGuards.AtLeast(_value, _value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Should_ReturnSuccess_WhenLonger()
    {
        var result = TimeSpanGuards.AtLeast(_value, _shorter);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtMost_Should_ReturnFailure_WhenLonger()
    {
        var result = TimeSpanGuards.AtMost(_value, _shorter);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_shorter);
    }

    [Test]
    public void AtMost_Should_ReturnSuccess_WhenEqual()
    {
        var result = TimeSpanGuards.AtMost(_value, _value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtMost_Should_ReturnSuccess_WhenShorter()
    {
        var result = TimeSpanGuards.AtMost(_value, _longer);
        result.IsValid.ShouldBeTrue();
    }
}