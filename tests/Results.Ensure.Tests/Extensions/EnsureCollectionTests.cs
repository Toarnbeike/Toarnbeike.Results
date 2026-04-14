using Toarnbeike.Results.Ensure.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class EnsureCollectionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        IEnumerable<string> list = [];
        var result = Ensure.NotEmpty(list);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.GuardName.ShouldBe("NotEmpty");
        failure.Expression.ShouldBe(nameof(list));
        failure.Constraint.ShouldBeNull();
        failure.AttemptedValue.ShouldBe(list);
        failure.Message.ShouldBe("'list' must not be empty.");
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenValid()
    {
        IEnumerable<int> list = [1, 2];
        var result = Ensure.NotEmpty(list);
        result.ShouldBeSuccess().ShouldBe(list);
    }
}