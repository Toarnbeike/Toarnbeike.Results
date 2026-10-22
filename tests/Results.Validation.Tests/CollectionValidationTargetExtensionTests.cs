using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Validation.Tests;

public class CollectionValidationTargetExtensionTests
{
    private readonly Result<List<string>> _result = Result.Success<List<string>>(["longitem", "medium", "short"]);
    private readonly Result<List<string>> _emptyResult = Result.Success<List<string>>([]);
    private readonly Result<List<string>> _failingResult = new TestFailure("Original failure");

    [Test]
    public void All_ShouldReturnFailure_WhenResultIsFailure()
    {
        var validationResult = _failingResult.ValidateCollection(items => items).All().MinLength(5);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void All_ShouldReturnFailure_WhenAnyItemFailsValidation()
    {
        var validationResult = _result.ValidateCollection(items => items).All().MinLength(6);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void All_ShouldReturnSuccess_WhenAllItemsPassValidation()
    {
        var validationResult = _result.ValidateCollection(items => items).All().MinLength(5);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void All_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var validationResult = _emptyResult.ValidateCollection(items => items).All().MinLength(5);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void All_ShouldReturnFailure_WhenResultIsFailure_WithSelector()
    {
        var validationResult = _failingResult.ValidateCollection(items => items).All(x => x).MinLength(5);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void All_ShouldReturnFailure_WhenAnyItemFailsValidation_WithSelector()
    {
        var validationResult = _result.ValidateCollection(items => items).All(x => x).MinLength(6);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void All_ShouldReturnSuccess_WhenAllItemsPassValidation_WithSelector()
    {
        var validationResult = _result.ValidateCollection(items => items).All(x => x).MinLength(5);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void All_ShouldReturnSuccess_WhenCollectionIsEmpty_WithSelector()
    {
        var validationResult = _emptyResult.ValidateCollection(items => items).All(x => x).MinLength(5);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Any_ShouldReturnFailure_WhenResultIsFailure()
    {
        var validationResult = _failingResult.ValidateCollection(items => items).Any().MinLength(5);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Any_ShouldReturnFailure_WhenAllItemsFailValidation()
    {
        var validationResult = _result.ValidateCollection(items => items).Any().MinLength(10);
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("No items satisfied the rule.");
    }

    [Test]
    public void Any_ShouldReturnSuccess_WhenAtLeastOneItemPassesValidation()
    {
        var validationResult = _result.ValidateCollection(items => items).Any().MinLength(6);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Any_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        var validationResult = _emptyResult.ValidateCollection(items => items).Any().MinLength(5);
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("No items satisfied the rule.");
    }

    [Test]
    public void Any_ShouldReturnFailure_WhenResultIsFailure_WithSelector()
    {
        var validationResult = _failingResult.ValidateCollection(items => items).Any(x => x).MinLength(5);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Any_ShouldReturnFailure_WhenAllItemsFailValidation_WithSelector()
    {
        var validationResult = _result.ValidateCollection(items => items).Any(x => x).MinLength(10);
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("No items satisfied the rule.");
    }

    [Test]
    public void Any_ShouldReturnSuccess_WhenAtLeastOneItemPassesValidation_WithSelector()
    {
        var validationResult = _result.ValidateCollection(items => items).Any(x => x).MinLength(6);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Any_ShouldReturnFailure_WhenCollectionIsEmpty_WithSelector()
    {
        var validationResult = _emptyResult.ValidateCollection(items => items).Any(x => x).MinLength(5);
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("No items satisfied the rule.");
    }

}
