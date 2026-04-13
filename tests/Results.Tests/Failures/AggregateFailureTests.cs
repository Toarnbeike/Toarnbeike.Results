using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Tests.Failures;

/// <summary>
/// Tests for the <see cref="AggregateFailureSummary"/> record.
/// </summary>
public class AggregateFailureTests
{
    [Test]
    public void AggregateFailure_Should_BeCreatedFromFailureIEnumerable()
    {
        var innerFailure1 = new ExceptionFailure(new ArgumentOutOfRangeException("arg1"));
        var innerFailure2 = new ValidationFailure("Property", "ValidationMessage");

        var failure = new AggregateFailureSummary([innerFailure1, innerFailure2]);

        failure.Category.ShouldBe(FailureCategory.System);
        failure.Message.ShouldBe("Multiple failures occurred");
        failure.Failures.Count.ShouldBe(2);
    }

    [Test]
    public void AggregateFailure_ShouldThrow_WhenCreatedWithNullFailures()
    {
        Should.Throw<ArgumentNullException>(() => new AggregateFailureSummary(null!));
    }

    [Test]
    public void AggregateFailure_ShouldThrow_WhenCreatedWithEmptyFailures()
    {
        Should.Throw<ArgumentException>(() => new AggregateFailureSummary([]));
    }

    [Test]
    public void AggregateFailure_Should_FlattenInnerAggregateFailures()
    {
        var innerFailure1 = new ExceptionFailure(new ArgumentOutOfRangeException("arg1"));
        var innerFailure2 = new AggregateFailureSummary([new ValidationFailure("Property", "ValidationMessage"), new TestFailure("test")]);
        var failure = new AggregateFailureSummary([innerFailure1, innerFailure2]);

        failure.Failures.Count.ShouldBe(3);
        failure.Failures.ShouldContain(innerFailure1);
        failure.Failures.ShouldContain(innerFailure2.Failures.First());
        failure.Failures.ShouldContain(innerFailure2.Failures.Last());
    }

    [Test]
    public void AggregateFailure_Should_BeAbleToChangeBaseProperties_UsingWithSyntax()
    {
        var innerFailure1 = new ExceptionFailure(new ArgumentOutOfRangeException("arg1"));
        var innerFailure2 = new ValidationFailure("Property", "ValidationMessage");

        var originalFailure = new AggregateFailureSummary([innerFailure1, innerFailure2]);

        var newFailure = originalFailure with { Message = "Something else" };
        newFailure.Message.ShouldBe("Something else");
    }
}