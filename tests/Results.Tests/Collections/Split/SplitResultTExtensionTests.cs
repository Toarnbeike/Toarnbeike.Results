using Toarnbeike.Results.Collections;

namespace Toarnbeike.Results.Tests.Collections.Split;

/// <summary>
/// Tests for the <see cref="SplitExtensions"/> class.
/// </summary>
public class SplitResultTExtensionTests : CollectionResultTExtensionTestBase
{
    [Test]
    public void Split_ShouldReturnSuccessAndEmptyCollections_WhenCollectionIsAllSuccesses()
    {
        var (successes, failures) = AllSuccessCollection.Split();

        successes.ShouldBe([1, 2, 3]);
        failures.ShouldBeEmpty();
    }

    [Test]
    public void Split_ShouldReturnSuccessAndFailureCollections_WhenCollectionContainsBoth()
    {
        var (successes, failures) = MixedCollection.Split();

        successes.ShouldBe([1, 3]);

        var actualFailures = failures.ToList();
        actualFailures.Count.ShouldBe(2);
        actualFailures.Select(f => f.Message).ShouldBe(["code1", "code2"]);
    }

    [Test]
    public void Split_ShouldReturnTwoEmptyCollections_WhenCollectionIsEmpty()
    {
        var (successes, failures) = EmptyCollection.Split();

        successes.ShouldBeEmpty();
        failures.ShouldBeEmpty();
    }

    [Test]
    public async Task SplitAsync_ShouldReturnSuccessAndEmptyCollections_WhenCollectionIsAllSuccesses()
    {
        var (successes, failures) = await AllSuccessTaskCollection.SplitAsync();

        successes.ShouldBe([1, 2, 3]);
        failures.ShouldBeEmpty();
    }

    [Test]
    public async Task SplitAsync_ShouldReturnSuccessAndFailureCollections_WhenCollectionContainsBoth()
    {
        var (successes, failures) = await MixedTaskCollection.SplitAsync();

        successes.ShouldBe([1, 3]);
        
        var actualFailures = failures.ToList();
        actualFailures.Count.ShouldBe(2);
        actualFailures.Select(f => f.Message).ShouldBe(["code1", "code2"]);
    }

    [Test]
    public async Task SplitAsync_ShouldReturnTwoEmptyCollections_WhenCollectionIsEmpty()
    {
        var (successes, failures) = await EmptyTaskCollection.SplitAsync();

        successes.ShouldBeEmpty();
        failures.ShouldBeEmpty();
    }
}
