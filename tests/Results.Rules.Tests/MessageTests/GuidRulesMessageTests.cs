using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests.MessageTests;

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class GuidRulesMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void NotEmpty_ShouldReturnCorrectMessage_ForEmptyGuid()
    {
        var value = Guid.Empty;
        var result = GuidRules.NotEmpty()(value);
        result.Message.ShouldBe("Value is not valid, must not be an empty Guid.");
    }

    [Test]
    public void IsVersion4_ShouldReturnCorrectMessage_ForNonVersion4Guid()
    {
        var value = Guid.CreateVersion7();
        var result = GuidRules.IsVersion4()(value);
        result.Message.ShouldBe($"'{value}' is not valid, must be a version 4 Guid, but is a version 7 Guid.");
    }

    [Test]
    public void IsVersion7_ShouldReturnCorrectMessage_ForNonVersion7Guid()
    {
        var value = Guid.NewGuid();
        var result = GuidRules.IsVersion7()(value);
        result.Message.ShouldBe($"'{value}' is not valid, must be a version 7 Guid, but is a version 4 Guid.");
    }
}
