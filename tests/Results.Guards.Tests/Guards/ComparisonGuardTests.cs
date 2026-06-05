using Toarnbeike.Results.Guards.Core.Guards;
using TUnit.FsCheck;

namespace Toarnbeike.Results.Guards.Tests.Guards;

public class ComparisonGuardTests
{
    [Test, FsCheckProperty]
    public bool GreaterThan_ShouldMatchDefinition(int value, int min)
    {
        return ComparisonGuards.GreaterThan(value, min) == (value > min);
    }

    [Test, FsCheckProperty]
    public bool AtLeast_ShouldMatchDefinition(int value, int min)
    {
        return ComparisonGuards.AtLeast(value, min) == (value >= min);
    }

    [Test, FsCheckProperty]
    public bool AtMost_ShouldMatchDefinition(int value, int max)
    {
        return ComparisonGuards.AtMost(value, max) == (value <= max);
    }

    [Test, FsCheckProperty]
    public bool LessThan_ShouldMatchDefinition(int value, int max)
    {
        return ComparisonGuards.LessThan(value, max) == (value < max);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldMatchDefinition(int value, int expected)
    {
        return ComparisonGuards.Equal(value, expected) == (value == expected);
    }

    [Test, FsCheckProperty]
    public bool NotEqual_ShouldMatchDefinition(int value, int expected)
    {
        return ComparisonGuards.NotEqual(value, expected) == (value != expected);
    }

    [Test, FsCheckProperty]
    public bool GreaterThan_ShouldBeInverseOfAtMost(int value, int boundary)
    {
        return ComparisonGuards.GreaterThan(value, boundary) != ComparisonGuards.AtMost(value, boundary);
    }

    [Test, FsCheckProperty]
    public bool LessThan_ShouldBeInverseOfAtLeast(int value, int boundary)
    {
        return ComparisonGuards.LessThan(value, boundary) != ComparisonGuards.AtLeast(value, boundary);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldBeInverseOfNotEqual(int value, int other)
    {
        return ComparisonGuards.Equal(value, other) != ComparisonGuards.NotEqual(value, other);
    }

    [Test, FsCheckProperty]
    public bool Equal_ShouldBeSymmetric(int value, int other)
    {
        return ComparisonGuards.Equal(value, other) == ComparisonGuards.Equal(other, value);
    }

    [Test, FsCheckProperty]
    public bool NotEqual_ShouldBeSymmetric(int value, int other)
    {
        return ComparisonGuards.NotEqual(value, other) == ComparisonGuards.NotEqual(other, value);
    }

    [Test]
    public void GreaterThan_Should_ReturnTrue_WhenGreater()
    {
        ComparisonGuards.GreaterThan(0.2f + 1e-6, 0.2f).ShouldBeTrue();
        ComparisonGuards.GreaterThan(TimeSpan.FromSeconds(61), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void GreaterThan_Should_ReturnFalse_WhenEqual()
    {
        ComparisonGuards.GreaterThan(0.2f, 0.2f).ShouldBeFalse();
        ComparisonGuards.GreaterThan(TimeSpan.FromSeconds(60), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void GreaterThan_Should_ReturnFalse_WhenLess()
    {
        ComparisonGuards.GreaterThan(0.2f - 1e-6, 0.2f).ShouldBeFalse();
        ComparisonGuards.GreaterThan(TimeSpan.FromSeconds(59), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Should_ReturnTrue_WhenGreater()
    {
        ComparisonGuards.AtLeast(0.2f + 1e-6, 0.2f).ShouldBeTrue();
        ComparisonGuards.AtLeast(TimeSpan.FromSeconds(61), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Should_ReturnTrue_WhenEqual()
    {
        ComparisonGuards.AtLeast(0.2f, 0.2f).ShouldBeTrue();
        ComparisonGuards.AtLeast(TimeSpan.FromSeconds(60), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Should_ReturnFalse_WhenLess()
    {
        ComparisonGuards.AtLeast(0.2f - 1e-6, 0.2f).ShouldBeFalse();
        ComparisonGuards.AtLeast(TimeSpan.FromSeconds(59), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Should_ReturnFalse_WhenGreater()
    {
        ComparisonGuards.AtMost(0.2f + 1e-6, 0.2f).ShouldBeFalse();
        ComparisonGuards.AtMost(TimeSpan.FromSeconds(61), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Should_ReturnTrue_WhenEqual()
    {
        ComparisonGuards.AtMost(0.2f, 0.2f).ShouldBeTrue();
        ComparisonGuards.AtMost(TimeSpan.FromSeconds(60), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Should_ReturnTrue_WhenLess()
    {
        ComparisonGuards.AtMost(0.2f - 1e-6, 0.2f).ShouldBeTrue();
        ComparisonGuards.AtMost(TimeSpan.FromSeconds(59), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void LessThan_Should_ReturnFalse_WhenGreater()
    {
        ComparisonGuards.LessThan(0.2f + 1e-6, 0.2f).ShouldBeFalse();
        ComparisonGuards.LessThan(TimeSpan.FromSeconds(61), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void LessThan_Should_ReturnFalse_WhenEqual()
    {
        ComparisonGuards.LessThan(0.2f, 0.2f).ShouldBeFalse();
        ComparisonGuards.LessThan(TimeSpan.FromSeconds(60), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void LessThan_Should_ReturnTrue_WhenLess()
    {
        ComparisonGuards.LessThan(0.2f - 1e-6, 0.2f).ShouldBeTrue();
        ComparisonGuards.LessThan(TimeSpan.FromSeconds(59), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void Equal_Should_ReturnFalse_WhenGreater()
    {
        ComparisonGuards.Equal(0.2f + 1e-6, 0.2f).ShouldBeFalse();
        ComparisonGuards.Equal(TimeSpan.FromSeconds(61), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void Equal_Should_ReturnTrue_WhenEqual()
    {
        ComparisonGuards.Equal(0.2f, 0.2f).ShouldBeTrue();
        ComparisonGuards.Equal(TimeSpan.FromSeconds(60), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void Equal_Should_ReturnFalse_WhenLess()
    {
        ComparisonGuards.Equal(0.2f - 1e-6, 0.2f).ShouldBeFalse();
        ComparisonGuards.Equal(TimeSpan.FromSeconds(59), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void NotEqual_Should_ReturnTrue_WhenGreater()
    {
        ComparisonGuards.NotEqual(0.2f + 1e-6, 0.2f).ShouldBeTrue();
        ComparisonGuards.NotEqual(TimeSpan.FromSeconds(61), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }

    [Test]
    public void NotEqual_Should_ReturnFalse_WhenNotEqual()
    {
        ComparisonGuards.NotEqual(0.2f, 0.2f).ShouldBeFalse();
        ComparisonGuards.NotEqual(TimeSpan.FromSeconds(60), TimeSpan.FromMinutes(1)).ShouldBeFalse();
    }

    [Test]
    public void NotEqual_Should_ReturnTrue_WhenLess()
    {
        ComparisonGuards.NotEqual(0.2f - 1e-6, 0.2f).ShouldBeTrue();
        ComparisonGuards.NotEqual(TimeSpan.FromSeconds(59), TimeSpan.FromMinutes(1)).ShouldBeTrue();
    }
}