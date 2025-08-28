using FluentValidation;

namespace Toarnbeike.Results.Messaging.Tests.TestData.Requests;

public sealed class TestCommandValidator : AbstractValidator<TestCommand>
{
    public TestCommandValidator()
    {
        RuleFor(x => x.Payload).NotEmpty();
    }
}