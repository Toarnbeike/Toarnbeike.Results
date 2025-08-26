using Toarnbeike.Results.Collections;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests.Collections;

/// <summary>
/// Tests for the <see cref="Results.Collections.CollectionExtensions"/> class.
/// </summary>
public class CollectionExtensionsTests
{
    private readonly List<Result> _allSuccessResults = [Result.Success(), Result.Success()];
    private readonly List<Result> _failingResults = [Result.Failure(new Failure("code1", "message1")), Result.Success(), Result.Failure(new Failure("code2", "message2"))];

    private readonly List<Result<int>> _allSuccessCollection = [1, 2, 3];
    private readonly List<Result<int>> _mixedCollection = [1, new Failure("collection", "value is missing"), 3, new Failure("second", "The second failure")];
    private readonly List<Result<int>> _emptyCollection = [];

    [Fact]
    public void AllSuccess_ShouldReturnTrue_WhenAllResultsAreSuccessful()
    {
        _allSuccessCollection.AllSuccess().ShouldBeTrue();
    }

    [Fact]
    public void AllSuccess_ShouldReturnFalse_WhenCollectionContainsFailures()
    {
        _mixedCollection.AllSuccess().ShouldBeFalse();
    }

    [Fact]
    public void AllSuccess_ShouldReturnTrue_WhenCollectionIsEmpty()
    {
        _emptyCollection.AllSuccess().ShouldBeTrue();
    }

    [Fact]
    public void Sequence_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = _allSuccessCollection.Sequence();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public void Sequence_ShouldReturnFailure_WhenCollectionContainsFailures()
    {
        var result = _mixedCollection.Sequence();
        result.ShouldBeFailureWithCode("collection"); // first failure in the collection
    }

    [Fact]
    public void Sequence_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = _emptyCollection.Sequence();
        result.ShouldBeSuccessWithValue([]);
    }

    [Fact]
    public void Aggregate_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = _allSuccessCollection.Aggregate();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public void Aggregate_ShouldReturnAggregateFailure_WhenCollectionContainsFailures()
    {
        var result = _mixedCollection.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count().ShouldBe(2);
    }

    [Fact]
    public void Aggregate_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = _emptyCollection.Aggregate();
        result.ShouldBeSuccessWithValue([]);
    }

    [Fact]
    public void Aggregate_ShouldReturnSuccess_WhenAllNonGenericResultsAreSuccessful()
    {
        var result = _allSuccessResults.Aggregate();
        result.ShouldBeSuccess();
    }

    [Fact]
    public void Aggregate_ShouldReturnAggregateFailure_WhenNonGenericResultCollectionContainsFailures()
    {
        var result = _failingResults.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count().ShouldBe(2);
    }

    [Fact]
    public void SuccessValues_ShouldReturnValues_WhenAllResultsAreSuccessful()
    {
        var result = _allSuccessCollection.SuccessValues();
        result.ShouldBe([1,2,3]);
    }

    [Fact]
    public void SuccessValues_ShouldReturnValues_WhenSomeResultsAreSuccessful()
    {
        var result = _mixedCollection.SuccessValues();
        result.ShouldBe([1,3]);
    }

    [Fact]
    public void SuccessValues_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = _emptyCollection.SuccessValues();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void Failures_ShouldReturnEmpty_WhenAllResultsAreSuccessful()
    {
        var result = _allSuccessCollection.Failures();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void Failures_ShouldReturnFailures_WhenSomeResultsAreFailures()
    {
        var result = _mixedCollection.Failures();
        result.Count().ShouldBe(2);
        result.Select(f => f.Code).ShouldBe(["collection", "second"]);
    }

    [Fact]
    public void Failures_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = _emptyCollection.Failures();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void Split_ShouldReturnSuccessAndEmptyCollections_WhenCollectionIsAllSuccesses()
    {
        var (successes, failures) = _allSuccessCollection.Split();

        successes.ShouldBe([1,2, 3]);
        failures.ShouldBeEmpty();
    }

    [Fact]
    public void Split_ShouldReturnSuccessAndFailureCollections_WhenCollectionContainsBoth()
    {
        var (successes, failures) = _mixedCollection.Split();
        
        successes.ShouldBe([1, 3]);
        failures.Count().ShouldBe(2);
        failures.Select(f => f.Code).ShouldBe(["collection", "second"]);
    }

    [Fact]
    public void Split_ShouldReturnTwoEmptyCollections_WhenCollectionIsEmpty()
    {
        var (successes, failures) = _emptyCollection.Split();

        successes.ShouldBeEmpty();
        failures.ShouldBeEmpty();
    }
}