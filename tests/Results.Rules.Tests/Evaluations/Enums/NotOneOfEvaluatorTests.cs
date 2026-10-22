using Toarnbeike.Results.Rules.Evaluations.Enums;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Enums;

public class NotOneOfEvaluatorTests
{
    [Test]
    public void Evaluate_ValidEnumValue_ReturnsValidResult()
    {
        var evaluator = new NotOneOfEvaluator<DayOfWeek>([DayOfWeek.Wednesday, DayOfWeek.Thursday]);
        var result = evaluator.Evaluate(DayOfWeek.Monday);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_InvalidEnumValue_ReturnsInvalidResult()
    {
        var evaluator = new NotOneOfEvaluator<DayOfWeek>([DayOfWeek.Wednesday, DayOfWeek.Thursday]);
        var result = evaluator.Evaluate(DayOfWeek.Wednesday);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        DayOfWeek[] rejectedValues = [DayOfWeek.Wednesday, DayOfWeek.Thursday];
        var evaluator = new NotOneOfEvaluator<DayOfWeek>(rejectedValues);
        var value = DayOfWeek.Monday;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("NotOneOfEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("enumType", nameof(DayOfWeek));
        result.EvaluationDetails.ShouldContainKeyAndValue("rejectedValues", "Wednesday, Thursday");
    }
}