namespace Toarnbeike.Results.Rules;

public static class GuidRules
{
    public static Rule<Guid> NotEmpty(string? message) =>
        new(value => value != Guid.Empty, message ?? "Guid must not be empty.");

    public static Rule<Guid> Version4(string? message) =>
        new(value => value != Guid.Empty && value.Version == 4, message ?? "Guid must be version 4.");

    public static Rule<Guid> Version7(string? message) =>
        new(value => value != Guid.Empty && value.Version == 7, message ?? "Guid must be version 7.");
}