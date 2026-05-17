using System.Numerics;
using Toarnbeike.Results.Ensure.Implementations_Obsolete;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class NumberGuardsToleranceTests
{
    // tolerances are tested using floats, and all types are tested in the DefaultTolerance tests.
    private readonly float _number = 1;
    private readonly float _smallGitter = 5e-7f;
    private readonly float _evenSmallerTolerance = 1e-7f;


    [Test]
    public void GreaterThanOrEqualTo_Should_SubtractDefaultTolerance()
    {
        var marginallyLess = _number + _smallGitter;
        var result = NumberGuards.GreaterThanOrEqualTo(_number, marginallyLess);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void GreaterThanOrEqualTo_Should_SubtractCustomTolerance()
    {
        var marginallyLess = _number + _smallGitter;
        var result = NumberGuards.GreaterThanOrEqualTo(_number, marginallyLess, _evenSmallerTolerance);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void LessThanOrEqualTo_Should_AddDefaultTolerance()
    {
        var marginallyGreater = _number - _smallGitter;
        var result = NumberGuards.LessThanOrEqualTo(_number, marginallyGreater);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void LessThanOrEqualTo_Should_AddCustomTolerance()
    {
        var marginallyGreater = _number - _smallGitter;
        var result = NumberGuards.LessThanOrEqualTo(_number, marginallyGreater, _evenSmallerTolerance);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void InRange_Should_SubtractDefaultTolerance_FromMaxSide()
    {
        var marginallyLess = _number - _smallGitter;
        var result = NumberGuards.InRange(_number, 0, marginallyLess);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void InRange_Should_SubtractCustomTolerance_FromMaxSide()
    {
        var marginallyLess = _number - _smallGitter;
        var result = NumberGuards.InRange(_number, 0, marginallyLess, _evenSmallerTolerance);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void InRange_Should_AddDefaultTolerance_ToMinSide()
    {
        var marginallyGreater = _number + _smallGitter;
        var result = NumberGuards.InRange(_number, marginallyGreater, 100000);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void InRange_Should_AddCustomTolerance_ToMinSide()
    {
        var marginallyGreater = _number + _smallGitter;
        var result = NumberGuards.InRange(_number, marginallyGreater, 100000, _evenSmallerTolerance);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Should_AddDefaultTolerance_RoundedToAbove()
    {
        var marginallyGreater = _number + _smallGitter;
        var result = NumberGuards.MultipleOf(marginallyGreater, _number);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Should_AddDefaultTolerance_RoundedToBelow()
    {
        var marginallyLess = _number - _smallGitter;
        var result = NumberGuards.MultipleOf(marginallyLess, _number);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Should_AddCustomTolerance_RoundedToAbove()
    {
        var somewhatBigger = _number + 1e-4;
        var result = NumberGuards.MultipleOf(somewhatBigger, _number, 1e-3);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Should_AddCustomTolerance_RoundedToBelow()
    {
        var somewhatBigger = _number - 1e-4;
        var result = NumberGuards.MultipleOf(somewhatBigger, _number, 1e-3);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void DefaultTolerance_ShouldBeSmall_ForFloatingPointTypes()
    {
        NumberGuards.DefaultTolerance<float>().ShouldBe(1e-6f);
        NumberGuards.DefaultTolerance<double>().ShouldBe(1e-9d);
        NumberGuards.DefaultTolerance<decimal>().ShouldBe(1e-9m);
        NumberGuards.DefaultTolerance<Half>().ShouldBe(Half.Parse("1e-2"));
    }

    [Test]
    public void Default_Tolerance_ShouldBeZero_ForWholeNumberTypes()
    {
        NumberGuards.DefaultTolerance<int>().ShouldBe(0);
        NumberGuards.DefaultTolerance<short>().ShouldBe((short)0);
        NumberGuards.DefaultTolerance<long>().ShouldBe(0);
        NumberGuards.DefaultTolerance<BigInteger>().ShouldBe(0);
        NumberGuards.DefaultTolerance<uint>().ShouldBe((uint)0);
        NumberGuards.DefaultTolerance<byte>().ShouldBe((byte)0);
    }
}