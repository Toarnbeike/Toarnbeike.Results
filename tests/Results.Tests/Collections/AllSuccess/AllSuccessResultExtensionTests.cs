using Toarnbeike.Results.Collections;

namespace Toarnbeike.Results.Tests.Collections.AllSuccess;

/// <summary>
/// Tests for the <see cref="AllSuccessExtensions"/> class.
/// </summary>
public class AllSuccessResultExtensionTests : CollectionResultExtensionTestBase
{
    [Test]
    public void AllSuccess_ShouldReturnTrue_WhenAllResultsAreSuccessful()
    {
        AllSuccessCollection.AllSuccess().ShouldBeTrue();
    }

    [Test]
    public void AllSuccess_ShouldReturnFalse_WhenCollectionContainsFailures()
    {
        MixedCollection.AllSuccess().ShouldBeFalse();
    }

    [Test]
    public void AllSuccess_ShouldReturnTrue_WhenCollectionIsEmpty()
    {
        EmptyCollection.AllSuccess().ShouldBeTrue();
    }

    [Test]
    public async Task AllSuccessAsync_ShouldReturnTrue_WhenAllResultsAreSuccessful()
    {
        (await AllSuccessTaskCollection.AllSuccessAsync()).ShouldBeTrue();
    }

    [Test]
    public async Task AllSuccessAsync_ShouldReturnFalse_WhenCollectionContainsFailures()
    {
        (await MixedTaskCollection.AllSuccessAsync()).ShouldBeFalse();
    }

    [Test]
    public async Task AllSuccessAsync_ShouldReturnTrue_WhenCollectionIsEmpty()
    {
        (await EmptyTaskCollection.AllSuccessAsync()).ShouldBeTrue();
    }
}