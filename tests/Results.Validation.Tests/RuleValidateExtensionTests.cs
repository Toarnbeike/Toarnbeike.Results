using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests;

public class RuleValidateExtensionTests
{
    [Test]
    public void Validate_ShouldReturnSuccess_WhenRulePasses()
    {
        var result = Result.Success("Hello");
        result
            .Validate(value => value).NotWhiteSpace()
            .ShouldBeSuccess();
    }

    [Test]
    public void Validate_ShouldReturnFailure_WhenRuleFails()
    {
        var result = Result.Success(" ");
        var failure = result
            .Validate(value => value).NotWhiteSpace()
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("value");
        failure.ValidationMessage.ShouldContain("not be whitespace");
    }

    [Test]
    public void Validate_ShouldReturnFailure_WithCustomPropertyName_WhenRuleFails()
    {
        var result = Result.Success(" ");
        var failure = result
            .Validate(value => value, propertyName: "CustomProperty").NotWhiteSpace()
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("CustomProperty");
        failure.ValidationMessage.ShouldContain("not be whitespace");
    }

    [Test]
    public void Validate_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        var result = Result<string>.Failure(new SimpleFailure("value", "Original failure."));
        var failure = result
            .Validate(value => value).NotWhiteSpace()
            .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void ValidateAll_ShouldReturnSuccess_WhenAllItemsInCollectionPassValidation()
    {
        var result = Result.Success(new TestClass { Numbers = [2, 4, 6] });
        result
            .ValidateAll(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeSuccess();
    }

    [Test]
    public void ValidateAll_ShouldReturnFailure_WhenAnyItemInCollectionFailsValidation()
    {
        var result = Result.Success(new TestClass { Numbers = [1, 2, 3] });
        var failure = result
            .ValidateAll(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("Numbers[0]");
        failure.ValidationMessage.ShouldContain("at least 2");
    }

    [Test]
    public void ValidateAll_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = Result.Success(new TestClass { Numbers = [] });
        result
            .ValidateAll(testClass => testClass.Numbers).AtLeast(1)
            .ShouldBeSuccess();
    }

    [Test]
    public void ValidateAll_ShouldReturnFailure_ForFailureInMiddleOfCollection()
    {
        var result = Result.Success(new TestClass { Numbers = [2, 1, 3] });
        var failure = result
            .ValidateAll(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("Numbers[1]");
        failure.ValidationMessage.ShouldContain("at least 2");
    }

    [Test]
    public void ValidateAll_ShouldReturnFirstFailure_WhenThereAreMultipleFailures()
    {
        var result = Result.Success(new TestClass { Numbers = [2, 1, -1] });
        var failure = result
            .ValidateAll(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeFailureOfType<ValidationFailure>(); // No ValidationFailureSummary
        failure.Property.ShouldBe("Numbers[1]");
    }

    [Test]
    public void ValidateAll_ShouldReturnFailure_WithCustomPropertyName_WhenRuleFails()
    {
        var result = Result.Success(new TestClass { Numbers = [1, 2, 3] });
        var failure = result
            .ValidateAll(testClass => testClass.Numbers, propertyName: "CustomNumbers").AtLeast(2)
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("CustomNumbers[0]");
        failure.ValidationMessage.ShouldContain("at least 2");
    }

    [Test]
    public void ValidateAll_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        var result = Result<TestClass>.Failure(new SimpleFailure("Numbers", "Original failure."));
        var failure = result
            .ValidateAll(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }

    [Test]
    public void ValidateAny_ShouldReturnSuccess_WhenAnyItemInCollectionPassesValidation()
    {
        var result = Result.Success(new TestClass { Numbers = [1, 2, 3] });
        result
            .ValidateAny(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeSuccess();
    }

    [Test]
    public void ValidateAny_ShouldReturnFailure_WhenNoItemsInCollectionPassValidation()
    {
        var result = Result.Success(new TestClass { Numbers = [1, 1, 1] });
        var failure = result
            .ValidateAny(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("Numbers");
        failure.ValidationMessage.ShouldContain("No items in the collection satisfied");
        failure.ValidationMessage.ShouldContain("at least 2");
    }

    [Test]
    public void ValidateAny_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        var result = Result.Success(new TestClass { Numbers = [] });
        var failure = result
            .ValidateAny(testClass => testClass.Numbers).AtLeast(1)
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("Numbers");
        failure.ValidationMessage.ShouldContain("No items in the collection satisfied");
        failure.ValidationMessage.ShouldContain("at least 1");
    }

    [Test]
    public void ValidateAny_ShouldReturnFailure_WithCustomPropertyName_WhenRuleFails()
    {
        var result = Result.Success(new TestClass { Numbers = [1, 1, 1] });
        var failure = result
            .ValidateAny(testClass => testClass.Numbers, propertyName: "CustomNumbers").AtLeast(2)
            .ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("CustomNumbers");
        failure.ValidationMessage.ShouldContain("No items in the collection satisfied");
        failure.ValidationMessage.ShouldContain("at least 2");
    }

    [Test]
    public void ValidateAny_ShouldReturnFailure_WhenIncomingResultIsFailure()
    {
        var result = Result<TestClass>.Failure(new SimpleFailure("Numbers", "Original failure."));
        var failure = result
            .ValidateAny(testClass => testClass.Numbers).AtLeast(2)
            .ShouldBeFailureOfType<SimpleFailure>();
        failure.Message.ShouldBe("Original failure.");
    }
}

public class TestClass
{
    public List<int> Numbers { get; set; } = [];
}
