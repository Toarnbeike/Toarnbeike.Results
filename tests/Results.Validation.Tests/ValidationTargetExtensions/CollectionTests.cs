using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Validation.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

  public class CollectionTests
{
    [Test]
    public void Contains_ShouldReturnSuccess_WhenItemIsInCollection()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).Contains(2);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Contains_ShouldReturnFailure_WhenItemIsNotInCollection()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).Contains(4);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Contains_ShouldReturnFailureWithCustomMessage_WhenItemIsNotInCollection()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).Contains(4, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void NotContains_ShouldReturnSuccess_WhenItemIsNotInCollection()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).NotContains(4);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotContains_ShouldReturnFailure_WhenItemIsInCollection()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).NotContains(2);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void NotContains_ShouldReturnFailureWithCustomMessage_WhenItemIsInCollection()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).NotContains(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void NotEmpty_ShouldReturnSuccess_WhenCollectionIsNotEmpty()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).NotEmpty();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailure_WhenCollectionIsEmpty()
    {
        Result<List<int>> result = new List<int>();
        var validationResult = result.ValidateCollection(x => x).NotEmpty();
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailureWithCustomMessage_WhenCollectionIsEmpty()
    {
        Result<List<int>> result = new List<int>();
        var validationResult = result.ValidateCollection(x => x).NotEmpty("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Empty_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        Result<List<int>> result = new List<int>();
        var validationResult = result.ValidateCollection(x => x).Empty();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Empty_ShouldReturnFailure_WhenCollectionIsNotEmpty()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).Empty();
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Empty_ShouldReturnFailureWithCustomMessage_WhenCollectionIsNotEmpty()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).Empty("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasAtLeast_ShouldReturnSuccess_WhenCollectionHasAtLeastSpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).HasAtLeast(2);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasAtLeast_ShouldReturnFailure_WhenCollectionHasFewerThanSpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1 };
        var validationResult = result.ValidateCollection(x => x).HasAtLeast(2);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void HasAtLeast_ShouldReturnFailureWithCustomMessage_WhenCollectionHasFewerThanSpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1 };
        var validationResult = result.ValidateCollection(x => x).HasAtLeast(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasAtMost_ShouldReturnSuccess_WhenCollectionHasAtMostSpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1, 2 };
        var validationResult = result.ValidateCollection(x => x).HasAtMost(2);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasAtMost_ShouldReturnFailure_WhenCollectionHasMoreThanSpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).HasAtMost(2);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void HasAtMost_ShouldReturnFailureWithCustomMessage_WhenCollectionHasMoreThanSpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1, 2, 3 };
        var validationResult = result.ValidateCollection(x => x).HasAtMost(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasBetween_ShouldReturnSuccess_WhenCollectionHasBetweenSpecifiedCounts()
    {
        Result<List<int>> result = new List<int> { 1, 2 };
        var validationResult = result.ValidateCollection(x => x).HasBetween(1, 3);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasBetween_ShouldReturnFailure_WhenCollectionHasFewerThanMinCount()
    {
        Result<List<int>> result = new List<int>();
        var validationResult = result.ValidateCollection(x => x).HasBetween(1, 3);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void HasBetween_ShouldReturnFailureWithCustomMessage_WhenCollectionHasFewerThanMinCount()
    {
        Result<List<int>> result = new List<int>();
        var validationResult = result.ValidateCollection(x => x).HasBetween(1, 3, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void HasExactly_ShouldReturnSuccess_WhenCollectionHasExactlySpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1, 2 };
        var validationResult = result.ValidateCollection(x => x).HasExactly(2);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void HasExactly_ShouldReturnFailure_WhenCollectionDoesNotHaveExactlySpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1 };
        var validationResult = result.ValidateCollection(x => x).HasExactly(2);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void HasExactly_ShouldReturnFailureWithCustomMessage_WhenCollectionDoesNotHaveExactlySpecifiedCount()
    {
        Result<List<int>> result = new List<int> { 1 };
        var validationResult = result.ValidateCollection(x => x).HasExactly(2, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }
}
