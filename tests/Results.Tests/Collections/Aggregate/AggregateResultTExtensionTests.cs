using Toarnbeike.Results.Collections;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Collections.Aggregate;

/// <summary>
/// Tests for the <see cref="Results.Collections.AggregateExtensions"/> class.
/// </summary>
public class AggregateResultTExtensionTests : CollectionResultTExtensionTestBase
{
    [Test]
    public void Aggregate_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = AllSuccessCollection.Aggregate();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Test]
    public void Aggregate_ShouldReturnAggregateFailure_WhenCollectionContainsFailures()
    {
        var result = MixedCollection.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count().ShouldBe(2);
    }

    [Test]
    public void Aggregate_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = EmptyCollection.Aggregate();
        result.ShouldBeSuccessWithValue([]);
    }

    [Test]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = await AllSuccessTaskCollection.AggregateAsync();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Test]
    public async Task AggregateAsync_ShouldReturnAggregateFailure_WhenCollectionContainsFailures()
    {
        var result = await MixedTaskCollection.AggregateAsync();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count.ShouldBe(2);
    }

    [Test]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = await EmptyTaskCollection.AggregateAsync();
        result.ShouldBeSuccessWithValue([]);
    }
}