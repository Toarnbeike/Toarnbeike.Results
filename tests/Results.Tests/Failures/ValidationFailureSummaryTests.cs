using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="ValidationFailureSummary"/> record.
/// </summary>
public class ValidationFailureSummaryTests
{
    private readonly ValidationFailure _validationFailure = new ValidationFailure("Property", "Something is wrong");
    private readonly ValidationFailureSummary _existingFailureSummary;

    public ValidationFailureSummaryTests()
    {
        _existingFailureSummary = new ValidationFailureSummary([_validationFailure]);
    }

    [Test]
    public void ValidationFailures_Should_BeCreatableFromASingleValidationFailure()
    {
        var result = new ValidationFailureSummary([_validationFailure]);
        result.Message.ShouldBe("One or more validation failures occured.");
        result.Category.ShouldBe(FailureCategory.Validation);
        result.Failures.Count.ShouldBe(1);
        result.Failures["Property"].Length.ShouldBe(1);
        result.Failures["Property"].Single().ShouldBe("Something is wrong");
    }

    [Test]
    public void ValidationFailures_ShouldThrow_WhenCreatedFromAnEmptyCollection()
    {
        Should.Throw<ArgumentException>(() => new ValidationFailureSummary([]));
    }

    [Test]
    public void GetFailuresFor_Should_ReturnFailuresIfFound()
    {
        IEnumerable<ValidationFailure> failures =
        [
            new("Property", "First"),
            new("Property2", "Second"),
            new("Property3", "Third"),
            new("Property3", "Fourth"),
        ];

        var validationFailures = new ValidationFailureSummary(failures);
        var property3Failures = validationFailures.GetFailuresFor("Property3");
        property3Failures.Count().ShouldBe(2);
    }

    [Test]
    public void GetFailuresFor_Should_ReturnEmptyCollectionIfNotFound()
    {
        IEnumerable<ValidationFailure> failures =
        [
            new("Property", "First"),
            new("Property2", "Second"),
            new("Property3", "Third"),
            new("Property3", "Fourth"),
        ];

        var validationFailures = new ValidationFailureSummary(failures);
        var property4Failures = validationFailures.GetFailuresFor("Property4").ToList();
        property4Failures.ShouldNotBeNull();
        property4Failures.ShouldBeEmpty();
    }

    [Test]
    public void ValidationFailures_Should_BeAbleToChangeBaseProperties_UsingWithExpression()
    {
        var newValidationFailures = _existingFailureSummary with { Message = "something else"};
        newValidationFailures.ShouldNotBeNull();
        newValidationFailures.Message.ShouldBe("something else");
    }
}
