using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class IntegerExtensionsTests
{
    private readonly int _value = 2;
    private readonly int _zero = 0;
    private readonly int _negativeValue = -2;

    [Test]
    public void GreaterThan_Should_ReturnFormattedFailure()
    {
        var result = _value.GreaterThan(3);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be greater than 3, but is 2."
        );
    }

    [Test]
    public void AtLeast_Should_ReturnFormattedFailure()
    {
        var result = _value.AtLeast(3);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at least 3, but is 2."
        );
    }

    [Test]
    public void AtMost_Should_ReturnFormattedFailure()
    {
        var result = _value.AtMost(1);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at most 1, but is 2."
        );
    }

    [Test]
    public void LessThan_Should_ReturnFormattedFailure()
    {
        var result = _value.LessThan(1);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be less than 1, but is 2."
        );
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure()
    {
        var result = _value.Between(3, 5);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be between 3 and 5, but is 2."
        );
    }

    [Test]
    public void Positive_Should_ReturnFormattedFailure()
    {
        var result = _negativeValue.Positive();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _negativeValue,
            expectedMessage: "'_negativeValue' must be positive, but is -2."
        );
    }

    [Test]
    public void AtLeastZero_Should_ReturnFormattedFailure()
    {
        var result = _negativeValue.AtLeastZero();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _negativeValue,
            expectedMessage: "'_negativeValue' must be at least zero, but is -2."
        );
    }

    [Test]
    public void AtMostZero_Should_ReturnFormattedFailure()
    {
        var result = _value.AtMostZero();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at most zero, but is 2."
        );
    }

    [Test]
    public void Negative_Should_ReturnFormattedFailure()
    {
        var result = _value.Negative();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be negative, but is 2."
        );
    }

    [Test]
    public void NotZero_Should_ReturnFormattedFailure()
    {
        var result = _zero.NotZero();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _zero,
            expectedMessage: "'_zero' must not be zero, but is."
        );
    }

    [Test]
    public void MultipleOf_Should_ReturnFormattedFailure()
    {
        var result = _value.MultipleOf(3);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be a multiple of 3, but is 2."
        );
    }

    [Test]
    public void Even_Should_ReturnFormattedFailure()
    {
        var value = 3;
        var result = value.Even();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: value,
            expectedMessage: "'value' must be an even number, but is 3."
        );
    }

    [Test]
    public void Odd_Should_ReturnFormattedFailure()
    {
        var result = _value.Odd();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be an odd number, but is 2."
        );
    }

    [Test]
    public void PowerOf2_Should_ReturnFormattedFailure()
    {
        var value = 3;
        var result = value.PowerOf2();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: value,
            expectedMessage: "'value' must be a power of 2, but is 3."
        );
    }

    [Test]
    public void Prime_Should_ReturnFormattedFailure()
    {
        var value = 4;
        var result = value.Prime();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: value,
            expectedMessage: "'value' must be a prime number, but is 4."
        );
    }
}