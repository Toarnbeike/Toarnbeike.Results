using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.TestHelpers;

/// <summary>
/// Tests for the <see cref="ResultSuccessAssertions"/>.
/// </summary>
public class ResultSuccessAssertionsTests
{
    [Test]
    public void ShouldBeSuccess_Passes_WhenResultIsSuccess()
    {
        var result = Result.Success();
        var act = () => result.ShouldBeSuccess();
        act.ShouldNotThrow();
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultIsNull()
    {
        Result result = null!;

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected result to be non-null.");
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultIsFailure()
    {
        Result result = new Failure("fail", "Failed");

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }

    [Test]
    public void ShouldBeSuccess_Passes_WhenResultIsSuccessOfTValue()
    {
        var result = Result.Success(42);
        var actual = Should.NotThrow(() => result.ShouldBeSuccess());

        actual.ShouldBe(42);
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultOfTValueIsNull()
    {
        Result<int> result = default!;

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected result to be non-null.");
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultIsFailureOfT()
    {
        Result<int> result = new Failure("fail", "Failed");

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }

    [Test]
    public void ShouldBeSuccessWithValue_Passes_WhenValueMatches()
    {
        var result = Result.Success(42);
        var value = result.ShouldBeSuccessWithValue(42);
        value.ShouldBe(42);
    }

    [Test]
    public void ShouldBeSuccessWithValue_Throws_WhenValueDiffers()
    {
        var result = Result.Success(99);

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeSuccessWithValue(42));

        ex.Message.ShouldBe("Expected success result with value '42', but got '99'.");
    }

    [Test]
    public void ShouldBeSuccessWithValue_ThrowsWithCustomMessage_WhenValueDiffers()
    {
        var result = Result.Success(99);

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeSuccessWithValue(42, "custom message"));

        ex.Message.ShouldBe("custom message");
    }

    [Test]
    public void ShouldBeSuccessWithValue_Passes_ForEqualArrays()
    {
        var result = Result.Success(new[] { 1, 2, 3 });

        Should.NotThrow(() => result.ShouldBeSuccessWithValue([1, 2, 3]));
    }

    [Test]
    public void ShouldBeSuccessWithValue_Passes_ForEqualLists()
    {
        var result = Result.Success(new List<string> { "a", "b" });

        Should.NotThrow(() => result.ShouldBeSuccessWithValue(["a", "b"]));
    }

    [Test]
    public void ShouldBeSuccessWithValue_Passes_ForEqualEnumerables()
    {
        IEnumerable<int> expected = [1,2,3];
        var result = Result.Success(expected);

        Should.NotThrow(() => result.ShouldBeSuccessWithValue([1,2,3]));
    }

    [Test]
    public void ShouldBeSuccessWithValue_Throws_WhenCollectionsDiffer()
    {
        var result = Result.Success(new[] { 1, 2, 3 });

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeSuccessWithValue([1, 2]));

        ex.Message.ShouldBe("Expected success result with value '[1, 2]', but got '[1, 2, 3]'.");
    }

    [Test]
    public void ShouldBeSuccessWithValue_Throws_WhenResultIsFailure()
    {
        Result<int> result = new Failure("fail", "Failed");

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccessWithValue(42));
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }

    [Test]
    public void ShouldBeSuccessThatSatisfiesPredicate_Passes_WhenPredicateMatches()
    {
        var result = Result.Success("hello");
        var value = result.ShouldBeSuccessThatSatisfiesPredicate(v => v.StartsWith('h'));
        value.ShouldBe("hello");
    }

    [Test]
    public void ShouldBeSuccessThatSatisfiesPredicate_Throws_WhenPredicateFails()
    {
        var result = Result.Success("hello");

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeSuccessThatSatisfiesPredicate(v => v.StartsWith('x')));

        ex.Message.ShouldBe("Expected success result with value that satisfies the predicate, but it did not.");
    }

    [Test]
    public void ShouldBeSuccessThatSatisfiesPredicate_ThrowsWithCustomMessage_WhenPredicateFails()
    {
        var result = Result.Success("hello");

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeSuccessThatSatisfiesPredicate(v => v.StartsWith('x'), "custom message"));

        ex.Message.ShouldBe("custom message");
    }

    [Test]
    public void ShouldBeSuccessThatSatisfiesPredicate_Throws_WhenResultIsFailure()
    {
        Result<string> result = new Failure("fail", "Failed");

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccessThatSatisfiesPredicate(v => v.StartsWith('x')));
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }
}