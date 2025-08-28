using FluentValidation;
using Microsoft.Extensions.Logging;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Messaging.Pipeline.Validation;
using Toarnbeike.Results.Messaging.Tests.Helpers;
using Toarnbeike.Results.Messaging.Tests.TestData.Requests;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Messaging.Tests.Pipeline.Validation;

public class FluentValidationPipelineBehaviourTests
{
    [Fact]
    public async Task PreHandleAsync_NoValidators_LogsAndReturnsSuccess()
    {
        var logger = new TestLogger<FluentValidationPipelineBehaviour<TestCommand, Result>>();
        var behaviour = new FluentValidationPipelineBehaviour<TestCommand, Result>(
            validators: [],
            logger: logger);

        var result = await behaviour.PreHandleAsync(new TestCommand());

        result.IsSuccess.ShouldBeTrue();
        logger.ShouldHaveMessage(LogLevel.Debug, "No validators registered");
    }

    [Fact]
    public async Task PreHandleAsync_ValidRequest_LogsValidAndReturnsSuccess()
    {
        var logger = new TestLogger<FluentValidationPipelineBehaviour<TestCommand, Result>>();
        var behaviour = new FluentValidationPipelineBehaviour<TestCommand, Result>(
            validators: [new TestCommandValidator()],
            logger: logger);

        var request = new TestCommand { Payload = "Valid" };
        var result = await behaviour.PreHandleAsync(request);

        result.IsSuccess.ShouldBeTrue();
        logger.ShouldHaveMessage(LogLevel.Debug, "Valid request");
    }

    [Fact]
    public async Task PreHandleAsync_InvalidRequest_LogsInvalidAndReturnsFailure()
    {
        var logger = new TestLogger<FluentValidationPipelineBehaviour<TestCommand, Result>>();
        var behaviour = new FluentValidationPipelineBehaviour<TestCommand, Result>(
            validators: [new TestCommandValidator()],
            logger: logger);

        var request = new TestCommand { Payload = "" };
        var result = await behaviour.PreHandleAsync(request);

        var validationFailures = result.ShouldBeFailureOfType<ValidationFailures>();
        validationFailures.Failures.Count.ShouldBe(1);

        logger.ShouldHaveMessage(LogLevel.Information, "Invalid request, 1 failure(s)");
    }
}