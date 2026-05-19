//using Toarnbeike.Results.Ensure.Extensions;
//using Toarnbeike.Results.TestExtensions;

//namespace Toarnbeike.Results.Ensure.Tests.Extensions;

//public class NumberExtensionsTests
//{
//    private readonly int _value = 5;
//    private readonly int _greater = 6;
//    private readonly int _less = 4;

//    private readonly string _customMessage = "Custom failure message";

//    [Test]
//    public void GreaterThan_Should_ReturnFormattedFailure()
//    {
//        var result = _value.GreaterThan(_greater);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("GreaterThan");
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be greater than {_greater}, but is {_value}.");
//    }

//    [Test]
//    public void GreaterThan_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.GreaterThan(_greater, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void GreaterThanOrEqualTo_Should_ReturnFormattedFailure()
//    {
//        var result = _value.AtLeast(_greater);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("AtLeast");
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be greater than or equal to {_greater}, but is {_value}.");
//    }

//    [Test]
//    public void GreaterThanOrEqualTo_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.AtLeast(_greater, message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void LessThanOrEqualTo_Should_ReturnFormattedFailure()
//    {
//        var result = _value.LessThanOrEqualTo(_less);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("LessThanOrEqualTo");
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be less than or equal to {_less}, but is {_value}.");
//    }

//    [Test]
//    public void LessThanOrEqualTo_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.LessThanOrEqualTo(_less, message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void LessThan_Should_ReturnFormattedFailure()
//    {
//        var result = _value.LessThan(_less);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("LessThan");
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be less than {_less}, but is {_value}.");
//    }

//    [Test]
//    public void LessThan_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.LessThan(_less, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void Positive_Should_ReturnFormattedFailure()
//    {
//        var value = -_value;
//        var result = value.Positive();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("Positive");
//        failure.Message.ShouldBe($"'{nameof(value)}' must be greater than or equal to 0, but is -5.");
//    }

//    [Test]
//    public void Positive_Should_ReturnCustomMessageFailure()
//    {
//        var value = -_value;
//        var result = value.Positive(message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void Negative_Should_ReturnFormattedFailure()
//    {
//        var result = _value.Negative();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("Negative");
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be less than or equal to 0, but is {_value}.");
//    }

//    [Test]
//    public void Negative_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.Negative(message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void InRange_Should_ReturnFormattedFailure_WhenTooLow()
//    {
//        var result = _value.InRange(_greater, _greater);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("InRange");
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be between {_greater} and {_greater}, but is {_value}.");
//    }

//    [Test]
//    public void InRange_Should_ReturnFormattedFailure_WhenTooHigh()
//    {
//        var result = _value.InRange(_less, _less);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be between {_less} and {_less}, but is {_value}.");
//    }

//    [Test]
//    public void InRange_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.InRange(_less, _less, message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void MultipleOf_Should_ReturnFormattedFailure()
//    {
//        var result = _value.MultipleOf(_less);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("MultipleOf");
//        failure.Message.ShouldBe($"'{nameof(_value)}' must be a multiple of {_less}, but is {_value}.");
//    }

//    [Test]
//    public void MultipleOf_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.MultipleOf(_less, message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }
//}