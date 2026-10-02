using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class GuidRules
{
    public static ValidationRule<Guid> NotEmpty(string? message) =>
        new(value => value != Guid.Empty, message ?? "Guid must not be empty.");

    public static ValidationRule<Guid> Version4(string? message) =>
        new(value => value != Guid.Empty && value.Version == 4, message ?? "Guid must be version 4.");

    public static ValidationRule<Guid> Version7(string? message) =>
    new(value => value != Guid.Empty && value.Version == 7, message ?? "Guid must be version 7.");
}