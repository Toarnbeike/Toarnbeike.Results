using Toarnbeike.Results.Collections;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Collections.Aggregate;

/// <summary>
/// Tests for the <see cref="Results.Collections.AggregateExtensions"/> class.
/// </summary>
public class AggregateResultExtensionTests : CollectionResultExtensionTestBase
{
    [Test]
    public void Aggregate_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = AllSuccessCollection.Aggregate();
        result.ShouldBeSuccess();
    }

    [Test]
    public void Aggregate_ShouldReturnAggregateFailure_WhenCollectionContainsFailures()
    {
        var result = MixedCollection.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count.ShouldBe(2);
    }

    [Test]
    public void Aggregate_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = EmptyCollection.Aggregate();
        result.ShouldBeSuccess();
    }

    [Test]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = await AllSuccessTaskCollection.AggregateAsync();
        result.ShouldBeSuccess();
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
        result.ShouldBeSuccess();
    }
}