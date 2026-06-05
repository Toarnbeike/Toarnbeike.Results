using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Tests.Guards;

public class GuidGuardTests
{
    [Test]
    public void NotEmpty_Should_ReturnTrue_ForAllBitsSetGuid()
    {
        GuidGuards.NotEmpty(Guid.AllBitsSet).ShouldBeTrue();
    }

    [Test]
    public void NotEmpty_Should_ReturnTrue_ForCreatedGuid()
    {
        GuidGuards.NotEmpty(Guid.NewGuid()).ShouldBeTrue();
    }

    [Test]
    public void NotEmpty_Should_ReturnFalse_ForEmptyGuid()
    {
        GuidGuards.NotEmpty(Guid.Empty).ShouldBeFalse();
    }

    [Test]
    public void IsVersion_Should_ReturnTrue_WhenVersionDoesMatch()
    {
        GuidGuards.IsVersion(Guid.NewGuid(), 4).ShouldBeTrue();
    }

    [Test]
    public void IsVersion_Should_ReturnFalse_WhenVersionDoesNotMatch()
    {
        GuidGuards.IsVersion(Guid.Empty, 4).ShouldBeFalse();
        GuidGuards.IsVersion(Guid.AllBitsSet, 4).ShouldBeFalse();
        GuidGuards.IsVersion(Guid.CreateVersion7(), 4).ShouldBeFalse();
    }
}