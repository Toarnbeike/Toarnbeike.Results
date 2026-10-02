namespace Toarnbeike.Results.Validation.Rules;

internal static class GuidRules
{
    public static bool NotEmpty(Guid value) => value != Guid.Empty;
    public static bool IsVersion(Guid value, int expectedVersion) => value.Version == expectedVersion;
}
