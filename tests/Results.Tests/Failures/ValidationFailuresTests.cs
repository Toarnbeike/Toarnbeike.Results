using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="ValidationFailures"/> record.
/// </summary>
public class ValidationFailuresTests
{
    private readonly ValidationFailure _validationFailure = new ValidationFailure("Property", "Something is wrong");
    private readonly ValidationFailures _existingFailures;

    public ValidationFailuresTests()
    {
        _existingFailures = new ValidationFailures([_validationFailure]);
    }

    [Fact]
    public void ValidationFailures_Should_BeCreatableFromASingleValidationFailure()
    {
        var result = new ValidationFailures([_validationFailure]);
        result.Code.ShouldBe("validation_failures");
        result.Message.ShouldBe("One or more validations failed:");
        result.Failures.Count.ShouldBe(1);
        result.Failures["Property"].Count.ShouldBe(1);
        result.Failures["Property"].Single().ShouldBe("Something is wrong");
    }

    [Fact]
    public void ValidationFailures_ShouldThrow_WhenCreatedFromAnEmptyCollection()
    {
        Should.Throw<ArgumentException>(() => new ValidationFailures([]));
    }

    [Fact]
    public void ValidationFailures_ShouldThrow_WhenCreatedUsingNullFailure()
    {
        Should.Throw<ArgumentException>(() => new ValidationFailures([_validationFailure, null!]));
    }

    [Fact]
    public void Add_Should_IncludeFailure_WithPropertyAndMessage()
    {
        var updatedFailures = _existingFailures.Add("newProperty", "Other failure");

        updatedFailures.Failures.Count.ShouldBe(2);
        updatedFailures.Failures["Property"].Count.ShouldBe(1);
        updatedFailures.Failures["newProperty"].Count.ShouldBe(1);
    }

    [Fact]
    public void Add_Should_IncludeFailure_FromValidationFailure()
    {
        var additionalValidationFailure = new ValidationFailure("Property", "Another failure");
        var updatedFailures = _existingFailures.Add(additionalValidationFailure);

        updatedFailures.Failures.Count.ShouldBe(1);
        updatedFailures.Failures["Property"].Count.ShouldBe(2);
        updatedFailures.Failures["Property"].Last().ShouldBe("Another failure");
    }

    [Fact]
    public void AddRange_Should_AddMultipleFailures_ForOneProperty()
    {
        List<string> additionalFailures = ["first", "second", "trird"];
        var updatedFailures = _existingFailures.AddRange("newProperty", additionalFailures);

        updatedFailures.Failures.Count.ShouldBe(2);
        updatedFailures.Failures["Property"].Count.ShouldBe(1);
        updatedFailures.Failures["newProperty"].Count.ShouldBe(3);
    }

    [Fact]
    public void Merge_Should_CombineFailures()
    {
        IEnumerable<ValidationFailure> failures =
        [
            new("Property", "First"),
            new("Property2", "Second"),
            new("Property3", "Third"),
            new("Property3", "Fourth"),
        ];

        var newFailures = new ValidationFailures(failures);

        var updatedFailures = _existingFailures.Merge(newFailures);

        updatedFailures.Failures.Count.ShouldBe(3);
        updatedFailures.Failures["Property"].Count.ShouldBe(2);
        updatedFailures.Failures["Property2"].Single().ShouldBe("Second");
        updatedFailures.Failures["Property3"].Count.ShouldBe(2);
    }

    [Fact]
    public void ToValidationFailureCollection_Should_CombineAllFailures()
    {
        IEnumerable<ValidationFailure> failures =
        [
            new("Property", "First"),
            new("Property2", "Second"),
            new("Property3", "Third"),
            new("Property3", "Fourth"),
        ];

        var validationFailures = new ValidationFailures(failures);
        var collection = validationFailures.ToValidationFailureCollection().ToList();
        collection.ShouldBeOfType<List<ValidationFailure>>();
        collection.Count.ShouldBe(4);
        collection.Last().ValidationMessage.ShouldBe("Fourth");
    }

    [Fact]
    public void GetfailuresFor_Should_ReturnFailuresIfFound()
    {
        IEnumerable<ValidationFailure> failures =
        [
            new("Property", "First"),
            new("Property2", "Second"),
            new("Property3", "Third"),
            new("Property3", "Fourth"),
        ];

        var validationFailures = new ValidationFailures(failures);
        var property3Failures = validationFailures.GetFailuresFor("Property3");
        property3Failures.Count().ShouldBe(2);
    }

    [Fact]
    public void GetfailuresFor_Should_ReturnEmptyCollectionIfNotFound()
    {
        IEnumerable<ValidationFailure> failures =
        [
            new("Property", "First"),
            new("Property2", "Second"),
            new("Property3", "Third"),
            new("Property3", "Fourth"),
        ];

        var validationFailures = new ValidationFailures(failures);
        var property4Failures = validationFailures.GetFailuresFor("Property4");
        property4Failures.ShouldNotBeNull();
        property4Failures.ShouldBeEmpty();
    }

    [Fact]
    public void ValidationFailures_Should_BeAbleToChangeBaseProperties_UsingWithExpression()
    {
        var newValidationFailures = _existingFailures with { Code = "something else"};
        newValidationFailures.ShouldNotBeNull();
        newValidationFailures.Code.ShouldBe("something else");
    }
}
