using Toarnbeike.Results.Linq;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests.Linq;

/// <summary>
/// Tests for <see cref="LinqExtensions"/>.
/// </summary>
public class LinqExtensionsTests
{
    [Fact]
    public void Select_Should_ProjectSuccessfulResult()
    {
        var result = from x in Result.Success(5)
                     select x * 2;

        result.ShouldBeSuccessWithValue(10);
    }

    [Fact]
    public void Select_should_propagate_failure()
    {
        var failure = Result<int>.Failure(new Failure("code", "error"));

        var result = from x in failure
                     select x * 2;

        result.ShouldBeFailureWithCode("code");
    }

    [Fact]
    public void SelectMany_Should_ChainSuccessfulResults()
    {
        var result = from x in Result.Success(2)
                     from y in Result.Success(3)
                     select x + y;

        result.ShouldBeSuccessWithValue(5);
    }

    [Fact]
    public void SelectMany_Should_PropagateFailure_FromFirstResult()
    {
        var result = from x in Result<int>.Failure(new Failure("first", "error"))
                     from y in Result.Success(3)
                     select x + y;

        result.ShouldBeFailureWithCode("first");
    }

    [Fact]
    public void SelectMany_Should_PropagateFailure_FromSecondResult()
    {
        var result = from x in Result.Success(2)
                     from y in Result<int>.Failure(new Failure("second", "error"))
                     select x + y;

        result.ShouldBeFailureWithCode("second");
    }

    [Fact]
    public void Where_Should_FilterSuccess_WhenPredicateIsTrue()
    {
        var result = from x in Result.Success(10)
                     where x > 5
                     select x;

        result.ShouldBeSuccessWithValue(10);
    }

    [Fact]
    public void Where_Should_CreateFailure_WhenPredicateIsFalse()
    {
        var result = from x in Result.Success(3)
                     where x > 5
                     select x;

        result.ShouldBeFailureWithCodeAndMessage("whereLinq", "LINQ predicate was not satisfied.");
    }

    [Fact]
    public void Where_Should_PropagateOriginalFailure()
    {
        var failed = Result<int>.Failure(new Failure("code", "error"));

        var result = from x in failed
                     where x > 5
                     select x;

        result.ShouldBeFailureWithCode("code");
    }

    [Fact]
    public void Let_keyword_should_preserve_value_across_bindings()
    {
        var result = from name in Result.Success("Alice")
                     let upper = name.ToUpper()
                     from reversed in Result.Success(new string(upper.Reverse().ToArray()))
                     select $"{upper} -> {reversed}";

        result.ShouldBeSuccessWithValue("ALICE -> ECILA");
    }
}
