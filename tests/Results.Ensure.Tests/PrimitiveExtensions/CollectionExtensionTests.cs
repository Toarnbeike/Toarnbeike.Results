using Toarnbeike.Results.Ensure.PrimitiveExtensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Ensure.Tests.PrimitiveExtensions;

public class CollectionExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        IEnumerable<string> list = [];
        var result = list.NotEmpty();
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
        var result = list.NotEmpty();
        result.ShouldBeSuccess().ShouldBe(list);
    }
}
