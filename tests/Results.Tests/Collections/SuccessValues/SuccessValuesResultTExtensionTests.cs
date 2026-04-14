using Toarnbeike.Results.Collections;

namespace Toarnbeike.Results.Tests.Collections.SuccessValues;

/// <summary>
/// Tests for the <see cref="SuccessValuesExtensions"/> class.
/// </summary>
public class SuccessValuesResultTExtensionTests : CollectionResultTExtensionTestBase
{
    [Test]
    public void SuccessValues_ShouldReturnValues_WhenAllResultsAreSuccessful()
    {
        var result = AllSuccessCollection.SuccessValues();
        result.ShouldBe([1, 2, 3]);
    }

    [Test]
    public void SuccessValues_ShouldReturnValues_WhenSomeResultsAreSuccessful()
    {
        var result = MixedCollection.SuccessValues();
        result.ShouldBe([1, 3]);
    }

    [Test]
    public void SuccessValues_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = EmptyCollection.SuccessValues();
        result.ShouldBeEmpty();
    }

    [Test]
    public async Task SuccessValuesAsync_ShouldReturnValues_WhenAllResultsAreSuccessful()
    {
        var result = await AllSuccessTaskCollection.SuccessValuesAsync();
        result.ShouldBe([1, 2, 3]);
    }

    [Test]
    public async Task SuccessValuesAsync_ShouldReturnValues_WhenSomeResultsAreSuccessful()
    {
        var result = await MixedTaskCollection.SuccessValuesAsync();
        result.ShouldBe([1, 3]);
    }

    [Test]
    public async Task SuccessValuesAsync_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = await EmptyTaskCollection.SuccessValuesAsync();
        result.ShouldBeEmpty();
    }
}
