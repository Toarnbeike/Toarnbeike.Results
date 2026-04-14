using Toarnbeike.Results.Collections;

namespace Toarnbeike.Results.Tests.Collections.Failures;

/// <summary>
/// Tests for the <see cref="FailuresExtensions"/> class.
/// </summary>
public class FailuresResultExtensionTests : CollectionResultExtensionTestBase
{
    [Test]
    public void Failures_ShouldReturnEmpty_WhenAllResultsAreSuccessful()
    {
        var result = AllSuccessCollection.Failures();
        result.ShouldBeEmpty();
    }

    [Test]
    public void Failures_ShouldReturnFailures_WhenSomeResultsAreFailures()
    {
        var result = MixedCollection.Failures().ToList();
        result.Count.ShouldBe(2);
        result.Select(f => f.Message).ShouldBe(["code1", "code2"]);
    }

    [Test]
    public void Failures_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = EmptyCollection.Failures();
        result.ShouldBeEmpty();
    }

    [Test]
    public async Task FailuresAsync_ShouldReturnEmpty_WhenAllResultsAreSuccessful()
    {
        var result = await AllSuccessTaskCollection.FailuresAsync();
        result.ShouldBeEmpty();
    }

    [Test]
    public async Task FailuresAsync_ShouldReturnFailures_WhenSomeResultsAreFailures()
    {
        var result = (await MixedTaskCollection.FailuresAsync()).ToList();
        result.Count.ShouldBe(2);
        result.Select(f => f.Message).ShouldBe(["code1", "code2"]);
    }

    [Test]
    public async Task FailuresAsync_ShouldReturnEmpty_WhenCollectionIsEmpty()
    {
        var result = await EmptyTaskCollection.FailuresAsync();
        result.ShouldBeEmpty();
    }
}
