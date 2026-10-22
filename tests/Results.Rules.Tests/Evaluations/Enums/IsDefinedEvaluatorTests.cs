using Toarnbeike.Results.Rules.Evaluations.Enums;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Enums;

public class IsDefinedEvaluatorTests
{
    [Test]
    public void Evaluate_ValidEnumValue_ReturnsValidResult()
    {
        var evaluator = new IsDefinedEvaluator<DayOfWeek>();
        var result = evaluator.Evaluate(DayOfWeek.Monday);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_InvalidEnumValue_ReturnsInvalidResult()
    {
        var evaluator = new IsDefinedEvaluator<DayOfWeek>();
        var invalidValue = (DayOfWeek)100; // Invalid enum value
        var result = evaluator.Evaluate(invalidValue);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new IsDefinedEvaluator<DayOfWeek>();
        var value = DayOfWeek.Wednesday;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("IsDefinedEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("enumType", nameof(DayOfWeek));
    }
}
