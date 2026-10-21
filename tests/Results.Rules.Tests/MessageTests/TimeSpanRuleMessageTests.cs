using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests.MessageTests;

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class StringRuleMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void NotEmpty_ShouldReturnCorrectMessage_ForEmptyString()
    {
        var result = StringRules.NotEmpty()("");
        result.Message.ShouldBe("'' is not valid, must not be empty.");
    }

    [Test]
    public void NotWhitespace_ShouldReturnCorrectMessage_ForWhitespaceString()
    {
        var result = StringRules.NotWhitespace()("   ");
        result.Message.ShouldBe("'   ' is not valid, must not be whitespace.");
    }

    [Test]
    public void MinLength_ShouldReturnCorrectMessage_ForStringShorterThanMinLength()
    {
        var result = StringRules.MinLength(5)("Hi");
        result.Message.ShouldBe("'Hi' is not valid, must be at least 5 characters long, but is 2.");
    }

    [Test]
    public void MaxLength_ShouldReturnCorrectMessage_ForStringLongerThanMaxLength()
    {
        var result = StringRules.MaxLength(5)("Hello, World!");
        result.Message.ShouldBe("'Hello, World!' is not valid, must be at most 5 characters long, but is 13.");
    }

    [Test]
    public void LengthBetween_ShouldReturnCorrectMessage_ForStringOutsideLengthRange()
    {
        var result = StringRules.LengthBetween(5, 10)("Hi");
        result.Message.ShouldBe("'Hi' is not valid, must be between 5 and 10 characters long, but is 2.");
    }

    [Test]
    public void IsEmailAddress_ShouldReturnCorrectMessage_ForInvalidEmail()
    {
        var result = StringRules.IsEmailAddress()("invalid-email");
        result.Message.ShouldBe("'invalid-email' is not valid, must be a valid email address.");
    }

    [Test]
    public void IsUri_ShouldReturnCorrectMessage_ForInvalidUri()
    {
        var result = StringRules.IsUri()("http://:bad-port/path");
        result.Message.ShouldBe("'http://:bad-port/path' is not valid, must be a valid URI.");
    }

    [Test]
    public void Matches_ShouldReturnCorrectMessage_ForNonMatchingString()
    {
        var result = StringRules.Matches(@"^\d+$", "description")("abc");
        result.Message.ShouldBe("'abc' is not valid, must match the pattern 'description'.");
    }
}

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class TimeSpanRuleMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void AtLeast_ShouldReturnCorrectMessage_ForValueLessThanMin()
    {
        var value = TimeSpan.FromSeconds(5);
        var result = TimeSpanRules.AtLeast(TimeSpan.FromSeconds(10))(value);
        result.Message.ShouldBe("'00:00:05' is not a valid TimeSpan, must be at least '00:00:10'.");
    }

    [Test]
    public void AtMost_ShouldReturnCorrectMessage_ForValueGreaterThanMax()
    {
        var value = TimeSpan.FromSeconds(15);
        var result = TimeSpanRules.AtMost(TimeSpan.FromSeconds(10))(value);
        result.Message.ShouldBe("'00:00:15' is not a valid TimeSpan, must be at most '00:00:10'.");
    }

    [Test]
    public void Between_ShouldReturnCorrectMessage_ForValueOutsideRange()
    {
        var value = TimeSpan.FromSeconds(5);
        var result = TimeSpanRules.Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20))(value);
        result.Message.ShouldBe("'00:00:05' is not a valid TimeSpan, must be between '00:00:10' and '00:00:20'.");
    }

    [Test]
    public void AtLeastZero_ShouldReturnCorrectMessage_ForNegativeValue()
    {
        var value = TimeSpan.FromSeconds(-5);
        var result = TimeSpanRules.AtLeastZero()(value);
        result.Message.ShouldBe("'-00:00:05' is not a valid TimeSpan, must be at least '00:00:00'.");
    }

}
