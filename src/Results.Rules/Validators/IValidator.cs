using System.Globalization;

namespace Toarnbeike.Results.Rules.Validators;

/// <summary>
/// Validator interface for validating values against a set of rules.
/// Use <see cref="Validator{T}"/> abstract class for implementing your own validators.
/// </summary>
/// <typeparam name="T">The type this validator validates</typeparam>
public interface IValidator<T>
{
    /// <summary>
    /// Validate the provided instance against the rules defined in this validator.
    /// </summary>
    /// <param name="value">The instance to validate.</param>
    /// <returns>The result of the validation. Either a successful result containing the incoming instance, or a validation failure summary.</returns>
    Result<T> Validate(T value);

    /// <summary>
    /// Get a description of the rules defined in this validator, split by property.
    /// This can be used to generate documentation for the validator.
    /// </summary>
    /// <param name="culture">The culture for localization.</param>
    /// <returns>A dictionary mapping property names to arrays of condition descriptions.</returns>
    Dictionary<string, string[]> GetConditions(CultureInfo culture);
}