using FluentValidation.Results;

namespace Toarnbeike.Results.FluentValidation;

/// <summary>
/// Provides extension methods to convert FluentValidation results into
/// <see cref="ValidationFailure"/> and <see cref="Failures.ValidationFailureSummary"/> used by the Results library.
/// </summary>
public static class FluentValidationConverters
{
    /// <summary>
    /// Converts a <see cref="ValidationFailure"/> from FluentValidation
    /// into a <see cref="Failures.ValidationFailure"/> used by the Results library.
    /// </summary>
    /// <param name="failure">The FluentValidation failure to convert.</param>
    public static Failures.ValidationFailure ToValidationFailure(this ValidationFailure failure)
    {
        return new Failures.ValidationFailure(failure.PropertyName, failure.ErrorMessage);
    }

    /// <summary>
    /// Converts a <see cref="ValidationResult"/> from FluentValidation
    /// into a <see cref="Failures.ValidationFailureSummary"/> instance.
    /// </summary>
    /// <param name="result">The FluentValidation result to convert.</param>
    public static Failures.ValidationFailureSummary ToValidationFailures(this ValidationResult result)
    {
        return new Failures.ValidationFailureSummary(result.Errors.Select(ToValidationFailure));
    }

    /// <summary>
    /// Converts a collection of FluentValidation <see cref="ValidationFailure"/> instances
    /// into a single <see cref="Failures.ValidationFailureSummary"/> instance.
    /// </summary>
    /// <param name="failures">The collection of FluentValidation failures.</param>
    public static Failures.ValidationFailureSummary ToValidationFailures(this IEnumerable<ValidationFailure> failures)
    {
        return new Failures.ValidationFailureSummary(failures.Select(ToValidationFailure));
    }
}