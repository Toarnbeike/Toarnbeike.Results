using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Tests.TypeRules;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class GuidRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void NotEmpty_ShouldReturnValidResult_ForNonEmptyGuid()
    {
        var value = Guid.NewGuid();
        var result = GuidRules.NotEmpty()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotEmpty_ShouldReturnInvalidResult_ForEmptyGuid()
    {
        var value = Guid.Empty;
        var result = GuidRules.NotEmpty()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotEmptyEvaluator");
        result.Message.ShouldContain("| Attempted Value: 00000000-0000-0000-0000-000000000000");
    }

    [Test]
    public void IsVersion4_ShouldReturnValidResult_ForVersion4Guid()
    {
        var value = Guid.NewGuid();
        var result = GuidRules.IsVersion4()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsVersion4_ShouldReturnInvalidResult_ForNonVersion4Guid()
    {
        var value = Guid.CreateVersion7();
        var result = GuidRules.IsVersion4()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualToEvaluator");
        result.Message.ShouldContain("| Attempted Value: 7");
        result.Message.ShouldContain($"originalValue: {value}");
    }

    [Test]
    public void IsVersion7_Should_ReturnValidResult_ForVersion7Guid()
    {
        var value = Guid.CreateVersion7();
        var result = GuidRules.IsVersion7()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsVersion7_ShouldReturnInvalidResult_ForNonVersion7Guid()
    {
        var value = Guid.NewGuid(); // Version 4 GUID
        var result = GuidRules.IsVersion7()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualToEvaluator");
        result.Message.ShouldContain("| Attempted Value: 4");
        result.Message.ShouldContain($"originalValue: {value}");
    }
}
