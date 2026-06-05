using FsCheck;
using Toarnbeike.Results.Guards.Core.Guards;
using TUnit.FsCheck;

namespace Toarnbeike.Results.Guards.Tests.Guards;

public class ToleranceGuardTests
{
    private readonly double _epsilon = 1e-9d;

    [Test, FsCheckProperty]
    public bool AtLeast_ShouldMatchDefinition(int value, int min, PositiveInt tolerance)
    {
        return ToleranceGuards.AtLeast(value, min, tolerance.Item)
               == (value >= min - tolerance.Item);
    }

    [Test, FsCheckProperty]
    public bool AtMost_ShouldMatchDefinition(int value, int max, PositiveInt tolerance)
    {
        return ToleranceGuards.AtMost(value, max, tolerance.Item)
               == (value <= max + tolerance.Item);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldBeSymmetric(int a, int b)
    {
        return ToleranceGuards.Equal(a, b, 0) == ToleranceGuards.Equal(b, a, 0);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldBeConsistent(int a, int b)
    {
        return ToleranceGuards.Equal(a, b, 0) == (a == b);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldBeReflexive(int value, PositiveInt tolerance)
    {
        return ToleranceGuards.Equal(value, value, tolerance.Item);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldMatchAbsoluteDistance(int a, int b, PositiveInt tolerance)
    {
        return ToleranceGuards.Equal(a, b, tolerance.Item) ==
               (Math.Abs(a - b) <= tolerance.Item);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldBeMonotonic_WithTolerance(int a, int b, PositiveInt tolerance, PositiveInt extra)
    {
        var t1 = tolerance.Item;
        var t2 = t1 + extra.Item;

        // Either a and b are not equal given t1, or, when already equal, also equal when t2 >= t1.
        return !ToleranceGuards.Equal(a, b, t1) || ToleranceGuards.Equal(a, b, t2);

    }

    [Test, FsCheckProperty]
    public bool NotEqual_ShouldBeInverseOfEqual(int a, int b, PositiveInt tolerance)
    {
        return ToleranceGuards.NotEqual(a, b, tolerance.Item)
               != ToleranceGuards.Equal(a, b, tolerance.Item);
    }

    [Test]
    public void AtLeast_Should_ReturnTrue_WhenEqual()
    {
        ToleranceGuards.AtLeast(10, 10, 0).ShouldBeTrue();
        ToleranceGuards.AtLeast(0.2, 0.2, 0).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Should_ReturnTrue_WhenOnEdgeOfTolerance()
    {
        ToleranceGuards.AtLeast(10,12,2).ShouldBeTrue();
        ToleranceGuards.AtLeast(0.2, 0.25, 0.05).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Should_ReturnFalse_WhenSmaller()
    {
        ToleranceGuards.AtLeast(10,11,0).ShouldBeFalse();
        ToleranceGuards.AtLeast(0.2 - _epsilon, 0.2, 0).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Should_Throw_OnNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ToleranceGuards.AtLeast(10, 10, -1));
    }

    [Test]
    public void AtMost_Should_ReturnFalse_WhenSmallerTolerance()
    {
        ToleranceGuards.AtLeast(10, 13, 2).ShouldBeFalse();
        ToleranceGuards.AtLeast(0.2 - _epsilon, 0.25, 0.05).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Should_ReturnTrue_WhenEqual()
    {
        ToleranceGuards.AtMost(10, 10, 0).ShouldBeTrue();
        ToleranceGuards.AtMost(0.2, 0.2, 0).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Should_ReturnTrue_WhenOnEdgeOfTolerance()
    {
        ToleranceGuards.AtMost(10, 8, 2).ShouldBeTrue();
        ToleranceGuards.AtMost(0.2, 0.15, 0.05).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Should_ReturnFalse_WhenLarger()
    {
        ToleranceGuards.AtMost(10, 9, 0).ShouldBeFalse();
        ToleranceGuards.AtMost(0.2 + _epsilon, 0.2, 0).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Should_ReturnFalse_WhenLargerTolerance()
    {
        ToleranceGuards.AtMost(10, 7, 2).ShouldBeFalse();
        ToleranceGuards.AtMost(0.2 + _epsilon, 0.15, 0.05).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Should_Throw_OnNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ToleranceGuards.AtMost(10, 10, -1));
    }

    [Test]
    public void Equal_Should_ReturnTrue_WhenEqual()
    {
        ToleranceGuards.Equal(10,10,0).ShouldBeTrue();
        ToleranceGuards.Equal(0.2,0.2,0).ShouldBeTrue();
    }

    [Test]
    public void Equal_Should_ReturnTrue_WhenOnEdgeOfPositiveTolerance()
    {
        ToleranceGuards.Equal(10, 11, 1).ShouldBeTrue();
        ToleranceGuards.Equal(0.2, 0.25, 0.05).ShouldBeTrue();
    }

    [Test]
    public void Equal_Should_ReturnTrue_WhenOnEdgeOfNegativeTolerance()
    {
        ToleranceGuards.Equal(10, 9, 1).ShouldBeTrue();
        ToleranceGuards.Equal(0.2, 0.15, 0.05).ShouldBeTrue();
    }

    [Test]
    public void Equal_Should_ReturnFalse_WhenOutsidePositiveTolerance()
    {
        ToleranceGuards.Equal(10, 12, 1).ShouldBeFalse();
        ToleranceGuards.Equal(0.2 - _epsilon, 0.25, 0.05).ShouldBeFalse();
    }

    [Test]
    public void Equal_Should_ReturnFalse_WhenOutsideNegativeTolerance()
    {
        ToleranceGuards.Equal(10, 8, 1).ShouldBeFalse();
        ToleranceGuards.Equal(0.2 + _epsilon, 0.15, 0.05).ShouldBeFalse();
    }

    [Test]
    public void Equal_Should_ReturnFalse_WhenSmaller()
    {
        ToleranceGuards.Equal(10, 11, 0).ShouldBeFalse();
        ToleranceGuards.Equal(0.2 - _epsilon, 0.2, 0).ShouldBeFalse();
    }

    [Test]
    public void Equal_Should_ReturnFalse_WhenLarger()
    {
        ToleranceGuards.Equal(10, 9, 0).ShouldBeFalse();
        ToleranceGuards.Equal(0.2 + _epsilon, 0.2, 0).ShouldBeFalse();
    }

    [Test]
    public void Equal_Should_Throw_OnNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ToleranceGuards.Equal(10, 10, -1));
    }

    [Test]
    public void NotEqual_Should_ReturnTrue_WhenOutsidePositiveTolerance()
    {
        ToleranceGuards.NotEqual(10, 12, 1).ShouldBeTrue();
        ToleranceGuards.NotEqual(0.2 - _epsilon, 0.25, 0.05).ShouldBeTrue();
    }

    [Test]
    public void NotEqual_Should_ReturnTrue_WhenOutsideNegativeTolerance()
    {
        ToleranceGuards.NotEqual(10, 8, 1).ShouldBeTrue();
        ToleranceGuards.NotEqual(0.2 + _epsilon, 0.15, 0.05).ShouldBeTrue();
    }

    [Test]
    public void NotEqual_Should_ReturnTrue_WhenSmaller()
    {
        ToleranceGuards.NotEqual(10, 11, 0).ShouldBeTrue();
        ToleranceGuards.NotEqual(0.2 - _epsilon, 0.2, 0).ShouldBeTrue();
    }

    [Test]
    public void NotEqual_Should_ReturnTrue_WhenLarger()
    {
        ToleranceGuards.NotEqual(10, 9, 0).ShouldBeTrue();
        ToleranceGuards.NotEqual(0.2 + _epsilon, 0.2, 0).ShouldBeTrue();
    }

    [Test]
    public void NotEqual_Should_ReturnFalse_WhenEqual()
    {
        ToleranceGuards.NotEqual(10, 10, 0).ShouldBeFalse();
        ToleranceGuards.NotEqual(0.2, 0.2, 0).ShouldBeFalse();
    }

    [Test]
    public void NotEqual_Should_ReturnFalse_WhenOnEdgeOfPositiveTolerance()
    {
        ToleranceGuards.NotEqual(10, 11, 1).ShouldBeFalse();
        ToleranceGuards.NotEqual(0.2, 0.25, 0.05).ShouldBeFalse();
    }

    [Test]
    public void NotEqual_Should_ReturnFalse_WhenOnEdgeOfNegativeTolerance()
    {
        ToleranceGuards.NotEqual(10, 9, 1).ShouldBeFalse();
        ToleranceGuards.NotEqual(0.2, 0.15, 0.05).ShouldBeFalse();
    }

    [Test]
    public void NotEqual_Should_Throw_OnNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ToleranceGuards.NotEqual(10, 10, -1));
    }
}