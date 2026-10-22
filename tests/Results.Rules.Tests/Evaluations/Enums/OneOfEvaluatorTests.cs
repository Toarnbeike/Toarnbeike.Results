using Toarnbeike.Results.Rules.Evaluations.Enums;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Enums;

public class OneOfEvaluatorTests
{
    [Test]
    public void Evaluate_ValidEnumValue_ReturnsValidResult()
    {
        var evaluator = new OneOfEvaluator<DayOfWeek>([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        var result = evaluator.Evaluate(DayOfWeek.Monday);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_InvalidEnumValue_ReturnsInvalidResult()
    {
        var evaluator = new OneOfEvaluator<DayOfWeek>([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        var result = evaluator.Evaluate(DayOfWeek.Wednesday);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        DayOfWeek[] acceptedValues = [DayOfWeek.Monday, DayOfWeek.Tuesday];
        var evaluator = new OneOfEvaluator<DayOfWeek>(acceptedValues);
        var value = DayOfWeek.Monday;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("OneOfEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("enumType", nameof(DayOfWeek));
        result.EvaluationDetails.ShouldContainKeyAndValue("acceptedValues", "Monday, Tuesday");
    }
}
