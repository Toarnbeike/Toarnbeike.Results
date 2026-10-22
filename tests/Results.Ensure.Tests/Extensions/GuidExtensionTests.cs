using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class GuidExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        var value = Guid.Empty;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotEmpty();

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty()
    {
        var value = Guid.NewGuid();
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotEmpty();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Version4_Should_ReturnSuccess_WhenVersion4()
    {
        var value = Guid.NewGuid();
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Version4();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Version4_Should_ReturnFailure_WhenNotVersion4()
    {
        var value = Guid.Empty;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Version4();

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }

    [Test]
    public void Version7_Should_ReturnSuccess_WhenVersion7()
    {
        var value = Guid.CreateVersion7();
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Version7();

        result.ShouldBeSuccess();
    }

    [Test]
    public void Version7_Should_ReturnFailure_WhenNotVersion7()
    {
        var value = Guid.NewGuid();
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).Version7();

        _ = result.ShouldBeFailureOfType<GuardFailure>();
    }
}
