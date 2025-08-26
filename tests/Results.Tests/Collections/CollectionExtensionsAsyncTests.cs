using Toarnbeike.Results.Collections;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests.Collections;

/// <summary>
/// Tests for the <see cref="Results.Collections.CollectionExtensions"/> class.
/// </summary>
public class CollectionExtensionsAsyncTests
{
    private readonly List<Task<Result>> _allSuccessResults = [Result.SuccessTask(), Result.SuccessTask()];
    private readonly List<Task<Result>> _failingResults = [Task.FromResult(Result.Failure(new Failure("code1", "message1"))), Result.SuccessTask(), Task.FromResult(Result.Failure(new Failure("code2", "message2")))];

    private readonly List<Task<Result<int>>> _allSuccessCollection = [Result.SuccessTask(1), Result.SuccessTask(2), Result.SuccessTask(3)];
    private readonly List<Task<Result<int>>> _mixedCollection = [Result.SuccessTask(1), Task.FromResult(Result<int>.Failure(new Failure("collection", "value is missing"))), 
        Result.SuccessTask(3), Task.FromResult(Result<int>.Failure(new Failure("second", "The second failure")))];
    private readonly List<Task<Result<int>>> _emptyCollection = [];

    [Fact]
    public async Task AllSuccessAsync_ShouldReturnTrue_WhenAllResultsAreSuccessful()
    {
        (await _allSuccessCollection.AllSuccessAsync()).ShouldBeTrue();
    }

    [Fact]
    public async Task AllSuccessAsync_ShouldReturnFalse_WhenCollectionContainsFailures()
    {
        (await _mixedCollection.AllSuccessAsync()).ShouldBeFalse();
    }

    [Fact]
    public async Task AllSuccessAsync_ShouldReturnTrue_WhenCollectionIsEmpty()
    {
        (await _emptyCollection.AllSuccessAsync()).ShouldBeTrue();
    }

    [Fact]
    public async Task SequenceAsync_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = await _allSuccessCollection.SequenceAsync();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public async Task SequenceAsync_ShouldReturnFailure_WhenCollectionContainsFailures()
    {
        var result = await _mixedCollection.SequenceAsync();
        result.ShouldBeFailureWithCode("collection"); // first failure in the collection
    }

    [Fact]
    public async Task SequenceAsync_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = await _emptyCollection.SequenceAsync();
        result.ShouldBeSuccessWithValue([]);
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = await _allSuccessCollection.AggregateAsync();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnAggregateFailure_WhenCollectionContainsFailures()
    {
        var result = await _mixedCollection.AggregateAsync();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count().ShouldBe(2);
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenCollectionIsEmpty()
    {
        var result = await _emptyCollection.AggregateAsync();
        result.ShouldBeSuccessWithValue([]);
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenAllNonGenericResultsAreSuccessful()
    {
        var result = await _allSuccessResults.AggregateAsync();
        result.ShouldBeSuccess();
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnAggregateFailure_WhenNonGenericResultCollectionContainsFailures()
    {
        var result = await _failingResults.AggregateAsync();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count().ShouldBe(2);
    }

    [Fact]
    public async Task SuccessValuesAsync_ShouldReturnValues_WhenAllResultsAreSuccessful()
    {
        var result = await _allSuccessCollection.SuccessValuesAsync();
        result.ShouldBe([1, 2, 3]);
    }

    [Fact]
    public async Task SuccessValuesAsync_ShouldReturnValues_WhenSomeResultsAreSuccessful()
    {
        var result = await _mixedCollection.SuccessValuesAsync();
        result.ShouldBe([1, 3]);
    }

    [Fact]
    public async Task SuccessValuesAsync_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = await _emptyCollection.SuccessValuesAsync();
        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task FailuresAsync_ShouldReturnEmpty_WhenAllResultsAreSuccessful()
    {
        var result = await _allSuccessCollection.FailuresAsync();
        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task FailuresAsync_ShouldReturnFailures_WhenSomeResultsAreFailures()
    {
        var result = await _mixedCollection.FailuresAsync();
        result.Count().ShouldBe(2);
        result.Select(f => f.Code).ShouldBe(["collection", "second"]);
    }

    [Fact]
    public async Task FailuresAsync_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = await _emptyCollection.FailuresAsync();
        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task SplitAsync_ShouldReturnSuccessAndEmptyCollections_WhenCollectionIsAllSuccesses()
    {
        var (successes, failures) = await _allSuccessCollection.SplitAsync();

        successes.ShouldBe([1, 2, 3]);
        failures.ShouldBeEmpty();
    }

    [Fact]
    public async Task SplitAsync_ShouldReturnSuccessAndFailureCollections_WhenCollectionContainsBoth()
    {
        var (successes, failures) = await _mixedCollection.SplitAsync();

        successes.ShouldBe([1, 3]);
        var actualFailures = failures.ToList();
        actualFailures.Count.ShouldBe(2);
        actualFailures.Select(f => f.Code).ShouldBe(["collection", "second"]);
    }

    [Fact]
    public async Task SplitAsync_ShouldReturnTwoEmptyCollections_WhenCollectionIsEmpty()
    {
        var (successes, failures) = await _emptyCollection.SplitAsync();

        successes.ShouldBeEmpty();
        failures.ShouldBeEmpty();
    }
}