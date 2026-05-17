using Toarnbeike.Results.Ensure.Implementations_Obsolete;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class PredicateGuardsTests
{
    private readonly int _value = 1;

    [Test]
    public void IsTrue_Should_ReturnFailure_WhenNotTrue()
    {
        var result = PredicateGuards.IsTrue(_value, IsNegative);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void IsTrue_Should_ReturnSuccess_WhenTrue()
    {
        var result = PredicateGuards.IsTrue(_value, IsPositive);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsFalse_Should_ReturnFailure_WhenNotFalse()
    {
        var result = PredicateGuards.IsFalse(_value, IsPositive);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void IsFalse_Should_ReturnSuccess_WhenFalse()
    {
        var result = PredicateGuards.IsFalse(_value, IsNegative);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public async Task IsTrueAsync_Should_ReturnFailure_WhenNotTrue()
    {
        var result = await PredicateGuards.IsTrueAsync(_value, IsNegativeAsync);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public async Task IsTrueAsync_Should_ReturnSuccess_WhenTrue()
    {
        var result = await PredicateGuards.IsTrueAsync(_value, IsPositiveAsync);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public async Task IsFalseAsync_Should_ReturnFailure_WhenNotFalse()
    {
        var result = await PredicateGuards.IsFalseAsync(_value, IsPositiveAsync);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public async Task IsFalseAsync_Should_ReturnSuccess_WhenFalse()
    {
        var result = await PredicateGuards.IsFalseAsync(_value, IsNegativeAsync);
        result.IsValid.ShouldBeTrue();
    }

    private static bool IsPositive(int value) => value > 0;
    private static bool IsNegative(int value) => value < 0;

    private static Task<bool> IsPositiveAsync(int value) => Task.FromResult(value > 0);
    private static Task<bool> IsNegativeAsync(int value) => Task.FromResult(value < 0);
}