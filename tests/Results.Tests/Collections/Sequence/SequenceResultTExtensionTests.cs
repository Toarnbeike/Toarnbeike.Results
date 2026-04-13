using Toarnbeike.Results.Collections;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Collections.Sequence;

/// <summary>
/// Tests for the <see cref="SequenceExtensions"/> class.
/// </summary>
public class SequenceResultTExtensionTests : CollectionResultTExtensionTestBase
{
    [Test]
    public void Sequence_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = AllSuccessCollection.Sequence();
        result.ShouldBeSuccess().ShouldBe([1, 2, 3]);
    }

    [Test]
    public void Sequence_ShouldReturnFailure_WhenCollectionContainsFailures()
    {
        var result = MixedCollection.Sequence();
        result.ShouldBeFailure().Code.ShouldBe("code1"); // first failure in the collection
    }

    [Test]
    public void Sequence_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = EmptyCollection.Sequence();
        result.ShouldBeSuccess().ShouldBeEmpty();
    }

    [Test]
    public async Task SequenceAsync_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = await AllSuccessTaskCollection.SequenceAsync();
        result.ShouldBeSuccess().ShouldBe([1, 2, 3]);
    }

    [Test]
    public async Task SequenceAsync_ShouldReturnFailure_WhenCollectionContainsFailures()
    {
        var result = await MixedTaskCollection.SequenceAsync();
        result.ShouldBeFailure().Code.ShouldBe("code1"); // first failure in the collection
    }

    [Test]
    public async Task SequenceAsync_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = await EmptyTaskCollection.SequenceAsync();
        result.ShouldBeSuccess().ShouldBeEmpty();
    }
}
