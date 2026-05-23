using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class GuidRuleTests
{
    private readonly Guid _empty = Guid.Empty;
    private readonly Guid _version4 = Guid.NewGuid();
    private readonly Guid _version7 = Guid.CreateVersion7();

    [Test]
    public void NotEmpty_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_empty).NotEmpty();
        result.AssertFailure(
            expectedAttemptedValue: _empty,
            expectedArgumentName: "_empty",
            expectedGuardName: "Guid.NotEmpty"
        );
    }

    [Test]
    public void Version4_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_version7).Version4();
        result.AssertFailure(
            expectedAttemptedValue: _version7,
            expectedArgumentName: "_version7",
            expectedGuardName: "Guid.Version4",
            ("Actual", 7)
        );
    }

    [Test]
    public void Version7_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_version4).Version7();
        result.AssertFailure(
            expectedAttemptedValue: _version4,
            expectedArgumentName: "_version4",
            expectedGuardName: "Guid.Version7",
            ("Actual", 4)
        );
    }
}