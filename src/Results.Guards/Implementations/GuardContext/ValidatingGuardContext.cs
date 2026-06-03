using System.Globalization;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Guards.Messages;
using Toarnbeike.Results.Guards.Tolerances;

namespace Toarnbeike.Results.Guards.Implementations.GuardContext;

internal sealed class ValidatingGuardContext(
    CultureInfo? culture = null,
    IToleranceProvider? toleranceProvider = null,
    TimeProvider? timeProvider = null) : GuardContextBase(toleranceProvider, timeProvider)
{
    public override IFailureMessageProvider FailureMessageProvider => MessageProviderFactory.GetForValidationFailures(culture);

    public override bool ShouldContinueExecution => true;

    protected override Result ToResult()
    {
        var failures = Results.Where(result => !result.IsValid).ToList();
        return failures.Count == 0
            ? Result.Success()
            : new ValidationFailureSummary(failures.Select(failure =>
            {
                var expression = failure.CustomExpression ?? failure.CapturedExpression;
                var property = expression.Contains('.')
                    ? expression[(expression.LastIndexOf('.') + 1)..]
                    : expression;
                return new ValidationFailure(property, CreateMessage(failure));
            }));
    }
}