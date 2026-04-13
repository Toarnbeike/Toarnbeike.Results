using Toarnbeike.Results.Linq;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Linq;

/// <summary>
/// Tests for <see cref="LinqExtensions"/>.
/// </summary>
public class LinqExtensionsTests
{
    [Test]
    public void Select_Should_ProjectSuccessfulResult()
    {
        var result = from x in Result.Success(5)
                     select x * 2;

        result.ShouldBeSuccess().ShouldBe(10);
    }

    [Test]
    public void Select_should_propagate_failure()
    {
        var failure = Result<int>.Failure(new TestFailure("failed"));

        var result = from x in failure
                     select x * 2;

        result.ShouldBeFailure().Message.ShouldBe("failed");
    }

    [Test]
    public void SelectMany_Should_ChainSuccessfulResults()
    {
        var result = from x in Result.Success(2)
                     from y in Result.Success(3)
                     select x + y;

        result.ShouldBeSuccess().ShouldBe(5);
    }

    [Test]
    public void SelectMany_Should_PropagateFailure_FromFirstResult()
    {
        var result = from x in Result<int>.Failure(new TestFailure("first"))
                     from y in Result.Success(3)
                     select x + y;

        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public void SelectMany_Should_PropagateFailure_FromSecondResult()
    {
        var result = from x in Result.Success(2)
                     from y in Result<int>.Failure(new TestFailure("second"))
                     select x + y;

        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public void Where_Should_FilterSuccess_WhenPredicateIsTrue()
    {
        var result = from x in Result.Success(10)
                     where x > 5
                     select x;

        result.ShouldBeSuccess().ShouldBe(10);
    }

    [Test]
    public void Where_Should_CreateFailure_WhenPredicateIsFalse()
    {
        var result = from x in Result.Success(3)
                     where x > 5
                     select x;

        result.ShouldBeFailure().Message.ShouldBe("LINQ predicate was not satisfied.");
    }

    [Test]
    public void Where_Should_PropagateOriginalFailure()
    {
        var failed = Result<int>.Failure(new TestFailure("failed"));

        var result = from x in failed
                     where x > 5
                     select x;

        result.ShouldBeFailure().Message.ShouldBe("failed");
    }

    [Test]
    public void Let_keyword_should_preserve_value_across_bindings()
    {
        var result = from name in Result.Success("Alice")
                     let upper = name.ToUpper()
                     from reversed in Result.Success(new string(upper.Reverse().ToArray()))
                     select $"{upper} -> {reversed}";

        result.ShouldBeSuccess().ShouldBe("ALICE -> ECILA");
    }
}
