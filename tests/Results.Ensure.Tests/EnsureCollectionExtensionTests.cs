using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests;

public class EnsureCollectionExtensionTests
{
    [Test]
    public void All_Should_ReturnFailure_WhenAnyItemFails()
    {
        var list = new List<string> { "item1", "item2", "item" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).All().MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void All_Should_ReturnSuccess_WhenAllItemsPass()
    {
        var list = new List<string> { "item1", "item2", "item3" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).All().MinLength(5);
        ensureResult.ShouldBeSuccess();
    }

    [Test]
    public void All_Should_ReturnFailure_WithCustomerPropertyName()
    {
        var list = new List<string> { "item1", "item2", "item" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list, "CustomPropertyName").All().MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("CustomPropertyName");
    }

    [Test]
    public void All_Should_ThrowNullException_WhenSelectorIsNull()
    {
        Result result = Result.Success();
        Should.Throw<ArgumentNullException>(() => result.EnsureCollection<string>(null!).All());
    }

    [Test]
    public void All_Should_ReturnFailure_WhenAnyItemFails_WithPropertySelector()
    {
        var list = new List<string> { "item1", "item2", "item" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).All(x => x).MinLength(5);
        _ = ensureResult.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void All_Should_ReturnSuccess_WhenAllItemsPass_WithPropertySelector()
    {
        var list = new List<string> { "item1", "item2", "item3" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).All(x => x).MinLength(5);
        ensureResult.ShouldBeSuccess();
    }

    [Test]
    public void All_Should_ReturnFailure_WithCustomerPropertyName_WithPropertySelector()
    {
        var list = new List<string> { "item1", "item2", "item" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list, "CustomPropertyName").All(x => x).MinLength(5);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("CustomPropertyName");
    }

    [Test]
    public void Any_Should_ReturnFailure_WhenAllItemsFail()
    {
        var list = new List<string> { "item1", "item2", "item3" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).Any().MinLength(6);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("No items in the collection satisfied the specified rule.");
    }

    [Test]
    public void Any_Should_ReturnSuccess_WhenAnyItemPasses()
    {
        var list = new List<string> { "item1", "2", "3" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).Any().MinLength(5);
        ensureResult.ShouldBeSuccess();
    }

    [Test]
    public void Any_Should_ReturnFailure_WithSelector_WhenAllItemsFail()
    {
        var list = new List<string> { "item1", "item2", "item3" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).Any(x => x).MinLength(6);
        var failure = ensureResult.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("No items in the collection satisfied the specified rule.");
    }

    [Test]
    public void Any_Should_ReturnSuccess_WithSelector_WhenAnyItemPasses()
    {
        var list = new List<string> { "1", "2", "item3" };
        Result result = Result.Success();
        var ensureResult = result.EnsureCollection(() => list).Any(x => x).MinLength(5);
        ensureResult.ShouldBeSuccess();
    }
}