using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class FloatingPointExtensionTests : InvariantCultureTestBase
{
    private readonly double _value = 1.5;
    private readonly double _negativeValue = -1.5;
    private readonly double _nan = double.NaN;
    private readonly double _tolerance = 0.4;

    [Test]
    public void GreaterThan_Should_ReturnFormattedFailure()
    {
        var result = _value.GreaterThan(3);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be greater than 3, but is 1.5."
        );
    }

    [Test]
    public void AtLeast_Should_ReturnFormattedFailure()
    {
        var result = _value.AtLeast(3, _tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at least 3 ± 0.4, but is 1.5."
        );
    }

    [Test]
    public void AtMost_Should_ReturnFormattedFailure()
    {
        var result = _value.AtMost(1, _tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at most 1 ± 0.4, but is 1.5."
        );
    }

    [Test]
    public void LessThan_Should_ReturnFormattedFailure()
    {
        var result = _value.LessThan(1);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be less than 1, but is 1.5."
        );
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure()
    {
        var result = _value.Between(3, 5, _tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be between 3 and 5 (± 0.4), but is 1.5."
        );
    }

    [Test]
    public void Positive_Should_ReturnFormattedFailure()
    {
        var result = _negativeValue.Positive();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _negativeValue,
            expectedMessage: "'_negativeValue' must be positive, but is -1.5."
        );
    }

    [Test]
    public void AtLeastZero_Should_ReturnFormattedFailure()
    {
        var result = _negativeValue.AtLeastZero(_tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _negativeValue,
            expectedMessage: "'_negativeValue' must be at least zero (± 0.4), but is -1.5."
        );
    }

    [Test]
    public void AtMostZero_Should_ReturnFormattedFailure()
    {
        var result = _value.AtMostZero(_tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be at most zero (± 0.4), but is 1.5."
        );
    }

    [Test]
    public void Negative_Should_ReturnFormattedFailure()
    {
        var result = _value.Negative();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be negative, but is 1.5."
        );
    }

    [Test]
    public void Zero_Should_ReturnFormattedFailure()
    {
        var result = _value.Zero(_tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be zero (± 0.4), but is 1.5."
        );
    }

    [Test]
    public void NotZero_Should_ReturnFormattedFailure()
    {
        var result = _value.NotZero(_value);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must not be zero (± 1.5), but is 1.5."
        );
    }

    [Test]
    public void MultipleOf_Should_ReturnFormattedFailure()
    {
        var result = _value.MultipleOf(2, _tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be a multiple of 2 (± 0.4), but is 1.5."
        );
    }

    [Test]
    public void WholeNumber_Should_ReturnFormattedFailure()
    {
        var result = _value.WholeNumber(_tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be a whole number (± 0.4), but is 1.5."
        );
    }

    [Test]
    public void MaxDecimalPlaces_Should_ReturnFormattedFailure()
    {
        var result = _value.MaxDecimalPlaces(0, _tolerance);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must have at most 0 decimal places (± 0.4), but is 1.5."
        );
    }

    [Test]
    public void Finite_Should_ReturnFormattedFailure()
    {
        var result = _nan.Finite();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _nan,
            expectedMessage: "'_nan' must be a finite number, but is not."
        );
    }

    [Test]
    public void NotNaN_Should_ReturnFormattedFailure()
    {
        var result = _nan.NotNaN();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _nan,
            expectedMessage: "'_nan' must not be NaN, but is."
        );
    }
}