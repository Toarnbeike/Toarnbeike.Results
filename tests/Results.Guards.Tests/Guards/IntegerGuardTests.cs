using FsCheck;
using Toarnbeike.Results.Guards.Core.Guards;
using TUnit.FsCheck;

namespace Toarnbeike.Results.Guards.Tests.Guards;

public class IntegerGuardTests
{
    [Test, FsCheckProperty]
    public bool MultipleOf_MatchesModulo(int value, NonZeroInt factor)
    {
        return IntegerGuards.MultipleOf(value, factor.Item) == (value % factor.Item == 0);
    }

    [Test, FsCheckProperty]
    public bool MultipleOf_ShouldBeMultipleOfSelf(NonZeroInt value)
    {
        return IntegerGuards.MultipleOf(value.Item, value.Item);
    }

    [Test, FsCheckProperty]
    public bool MultipleOf_ShouldSucceedForMultiplier(int multiplier, NonZeroInt factor)
    {
        var value = multiplier * factor.Item;
        return IntegerGuards.MultipleOf(value, factor.Item);
    }

    [Test, FsCheckProperty]
    public bool Even_ShouldSuccessForMultipleOfTwo(NonZeroInt multiplier)
    {
        var value = multiplier.Item * 2;
        return IntegerGuards.Even(value);
    }

    [Test, FsCheckProperty]
    public bool Even_ShouldBeInverseOfOdd(int value)
    {
        return IntegerGuards.Even(value) != IntegerGuards.Odd(value);
    }

    [Test, FsCheckProperty]
    public bool Odd_ShouldSuccessForMultipleOfTwoPlusOne(int multiplier)
    {
        var value = multiplier * 2 + 1;
        return IntegerGuards.Odd(value);
    }

    [Test, FsCheckProperty]
    public bool Prime_ShouldNeverBeEven_ExceptFor2(PositiveInt positiveValue)
    {
        var value = positiveValue.Item;

        if (IntegerGuards.Even(value) && value != 0 && value != 2) return !IntegerGuards.Prime(value);
        return true;
    }

    [Test, FsCheckProperty]
    public bool PowerOf2_ShouldMatchDefinition(int value)
    {
        return IntegerGuards.PowerOf2(value) == 
               (Math.IEEERemainder(Math.Log2(value), 1) == 0);
    }

    [Test]
    public void MultipleOf_Should_ReturnTrue_WhenMultipleOf()
    {
        IntegerGuards.MultipleOf(8,2).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Should_ReturnFalse_WhenNoMultiple()
    {
        IntegerGuards.MultipleOf(2,8).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Should_ThrowException_WhenFactorIsZero()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => IntegerGuards.MultipleOf(2, 0));
    }

    [Test]
    public void MultipleOf_Should_ReturnTrue_WhenNegativeMultipleOf()
    {
        IntegerGuards.MultipleOf(-8, -2).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Should_ReturnTrue_WhenNegativeMultipleOf_AndValueIsPositive()
    {
        IntegerGuards.MultipleOf(8, -2).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Should_ReturnTrue_WhenValueIsNegative()
    {
        IntegerGuards.MultipleOf(-8, 2).ShouldBeTrue();
    }

    [Test]
    public void Prime_Should_ReturnFalse_ForDefinedNotPrimeNumbers()
    {
        IntegerGuards.Prime(0).ShouldBeFalse();
        IntegerGuards.Prime(1).ShouldBeFalse();
    }

    [Test]
    public void Prime_Should_ReturnTrue_ForPrimeNumbers()
    {
        IntegerGuards.Prime(2).ShouldBeTrue();
        IntegerGuards.Prime(3).ShouldBeTrue();
        IntegerGuards.Prime(5).ShouldBeTrue();
    }

    [Test]
    public void Prime_Should_ReturnFalse_ForNonPrimeNumbers()
    {
        IntegerGuards.Prime(4).ShouldBeFalse();
        IntegerGuards.Prime(6).ShouldBeFalse();
        IntegerGuards.Prime(2_147_483_643).ShouldBeFalse();
    }

    [Test]
    public void Prime_Should_ReturnTrue_ForLargePrimeNumber()
    {
        IntegerGuards.Prime(999331).ShouldBeTrue();
    }
}
