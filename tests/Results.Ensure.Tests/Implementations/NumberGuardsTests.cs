using Toarnbeike.Results.Ensure.Implementations_Obsolete;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class NumberGuardsTests
{
    private readonly int _number = 10;
    private readonly int _greater = 11;
    private readonly int _less = 9;

    [Test]
    public void GreaterThan_Should_ReturnFailure_WhenOtherIsGreater()
    {
        var result = NumberGuards.GreaterThan(_number, _greater);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_greater);
    }

    [Test]
    public void GreaterThan_Should_ReturnFailure_WhenOtherIsEqual()
    {
        var result = NumberGuards.GreaterThan(_number, _number);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void GreaterThan_Should_ReturnSuccess_WhenOtherIsLess()
    {
        var result = NumberGuards.GreaterThan(_number, _less);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void GreaterThanOrEqualTo_Should_ReturnFailure_WhenOtherIsGreater()
    {
        var result = NumberGuards.GreaterThanOrEqualTo(_number, _greater);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_greater);
    }

    [Test]
    public void GreaterThanOrEqualTo_Should_ReturnSuccess_WhenOtherIsEqual()
    {
        var result = NumberGuards.GreaterThanOrEqualTo(_number, _number);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void GreaterThanOrEqualTo_Should_ReturnSuccess_WhenOtherIsLess()
    {
        var result = NumberGuards.GreaterThanOrEqualTo(_number, _less);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void LessThanOrEqualTo_Should_ReturnFailure_WhenOtherIsLess()
    {
        var result = NumberGuards.LessThanOrEqualTo(_number, _less);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_less);
    }

    [Test]
    public void LessThanOrEqualTo_Should_ReturnSuccess_WhenOtherIsEqual()
    {
        var result = NumberGuards.LessThanOrEqualTo(_number, _number);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void LessThanOrEqualTo_Should_ReturnSuccess_WhenOtherIsGreater()
    {
        var result = NumberGuards.LessThanOrEqualTo(_number, _greater);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void LessThan_Should_ReturnFailure_WhenOtherIsLess()
    {
        var result = NumberGuards.LessThan(_number, _less);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_less);
    }

    [Test]
    public void LessThan_Should_ReturnFailure_WhenOtherIsEqual()
    {
        var result = NumberGuards.LessThan(_number, _number);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void LessThan_Should_ReturnSuccess_WhenOtherIsGreater()
    {
        var result = NumberGuards.LessThan(_number, _greater);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void InRange_Should_ReturnFailure_WhenActualIsSmallerThanRange()
    {
        var result = NumberGuards.InRange(_number, _greater, _greater);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe((_greater, _greater));
    }

    [Test]
    public void InRange_Should_ReturnFailure_WhenActualIsGreaterThanRange()
    {
        var result = NumberGuards.InRange(_number, _less, _less);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void InRange_Should_ReturnSuccess_WhenActualIsEqualToMin()
    {
        var result = NumberGuards.InRange(_number, _number, _greater);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void InRange_Should_ReturnSuccess_WhenActualIsEqualToMax()
    {
        var result = NumberGuards.InRange(_number, _less, _number);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void InRange_Should_ReturnSuccess_WhenActualIsBetweenMinAndMax()
    {
        var result = NumberGuards.InRange(_number, _less, _greater);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Should_ReturnFailure_WhenNotAMultiple()
    {
        var result = NumberGuards.MultipleOf(_number, 3);
        result.Constraint.ShouldBe(3);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Should_ReturnSuccess_WhenMultiple()
    {
        var result = NumberGuards.MultipleOf(_number, 5);
        result.IsValid.ShouldBeTrue();
    }
}