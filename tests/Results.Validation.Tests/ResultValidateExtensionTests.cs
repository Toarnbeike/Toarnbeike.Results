using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Validation.Tests.Testdata;

namespace Toarnbeike.Results.Validation.Tests;

public class ResultValidateExtensionTests
{
    [Test]
    public void ValidateUsing_ShouldRetainFailure_WhenResultIsFailure()
    {
        Result<Customer> result = new TestFailure("Original failure");
        var validatedResult = result.ValidateUsing(new CustomerValidator());
        var failure = validatedResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original failure");
    }

    [Test]
    public void ValidateUsing_ShouldReturnFailure_WhenValidationFails()
    {
        Result<Customer> result = Customer.Valid with { Name= "A" };
        var validatedResult = result.ValidateUsing(new CustomerValidator());
        var failure = validatedResult.ShouldBeFailureOfType<ValidationFailureSummary>();
        failure.GetFailuresFor("Name").ShouldNotBeNull();
    }

    [Test]
    public void ValidateUsing_ShouldReturnSuccess_WhenValidationPasses()
    {
        Result<Customer> result = Customer.Valid;
        var validatedResult = result.ValidateUsing(new CustomerValidator());
        validatedResult.ShouldBeSuccess();
    }

    [Test]
    public void Validate_ShouldRetainFailure_WithoutSelector_WhenResultIsFailure()
    {
        Result<string> result = new TestFailure("Original failure");
        var validatedResult = result.Validate("propName").MinLength(5);
        var failure = validatedResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original failure");
    }

    [Test]
    public void Validate_ShouldReturnFailure_WithoutSelector_WhenValidationFails()
    {
        Result<string> result = "Hi";
        var validatedResult = result.Validate("propName").MinLength(5);
        var failure = validatedResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("propName");
    }

    [Test]
    public void Validate_ShouldReturnSuccess_WithoutSelector_WhenValidationPasses()
    {
        Result<string> result = "Hello";
        var validatedResult = result.Validate("propName").MinLength(5);
        validatedResult.ShouldBeSuccess();
    }

    [Test]
    public void Validate_ShouldRetainFailure_WithSelector_WhenResultIsFailure()
    {
        Result<string> result = new TestFailure("Original failure");
        var validatedResult = result.Validate(x => x).MinLength(5);
        var failure = validatedResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original failure");
    }

    [Test]
    public void Validate_ShouldReturnFailure_WithSelector_WhenValidationFails()
    {
        Result<string> result = "Hi";
        var validatedResult = result.Validate(x => x).MinLength(5);
        var failure = validatedResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("x");
    }

    [Test]
    public void Validate_ShouldReturnFailure_WithSelector_WithCustomPropertyName_WhenProvided()
    {
        Result<string> result = "Hi";
        var validatedResult = result.Validate(x => x, propertyName: "CustomProperty").MinLength(5);
        var failure = validatedResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("CustomProperty");
    }


    [Test]
    public void Validate_ShouldReturnSuccess_WithSelector_WhenValidationPasses()
    {
        Result<string> result = "Hello";
        var validatedResult = result.Validate(x => x).MinLength(5);
        validatedResult.ShouldBeSuccess();
    }

    [Test]
    public void ValidateCollection_ShouldRetainFailure_WhenResultIsFailure()
    {
        Result<IEnumerable<string>> result = new TestFailure("Original failure");
        var validatedResult = result.ValidateCollection(x => x).NotEmpty();
        var failure = validatedResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original failure");
    }

    [Test]
    public void ValidateCollection_ShouldReturnFailure_WhenValidationFails()
    {
        Result<IEnumerable<string>> result = new List<string>();
        var validatedResult = result.ValidateCollection(x => x).NotEmpty();
        var failure = validatedResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("x");
    }

    [Test]
    public void ValidateCollection_ShouldReturnFailure_WithCustomPropertyName_WhenProvided()
    {
        Result<IEnumerable<string>> result = new List<string>();
        var validatedResult = result.ValidateCollection(x => x, propertyName: "CustomCollection").NotEmpty();
        var failure = validatedResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.Property.ShouldBe("CustomCollection");
    }

    [Test]
    public void ValidateCollection_ShouldReturnSuccess_WhenValidationPasses()
    {
        Result<IEnumerable<string>> result = new List<string> { "Item1" };
        var validatedResult = result.ValidateCollection(x => x).NotEmpty();
        validatedResult.ShouldBeSuccess();
    }

    [Test]
    public void ValidateCollection_ShouldThrowArgumentNullException_WhenSelectorIsNull()
    {
        Result<IEnumerable<string>> result = new List<string>();
        Assert.Throws<ArgumentNullException>(() => result.ValidateCollection<IEnumerable<string>, string>(null!).HasAtLeast(1));
    }
}
