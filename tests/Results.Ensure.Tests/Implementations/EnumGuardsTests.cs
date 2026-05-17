using Toarnbeike.Results.Ensure.Implementations_Obsolete;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class EnumGuardsTests
{
    private enum TestEnum
    {
        Defined
    }

    [Test]
    public void IsDefined_Should_ReturnFailure_WhenNotDefined()
    {
        var undefined = (TestEnum)2;
        var result = EnumGuards.IsDefined(undefined);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void IsDefined_Should_ReturnSuccess_WhenDefined()
    {
        var result = EnumGuards.IsDefined(TestEnum.Defined);
        result.IsValid.ShouldBeTrue();
    }
}