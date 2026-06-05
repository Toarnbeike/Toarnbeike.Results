namespace Toarnbeike.Results.Guards.Core.Guards;

internal static class GuidGuards
{
    public static bool NotEmpty(Guid value) => value != Guid.Empty;
    public static bool IsVersion(Guid value, int expectedVersion) => value.Version == expectedVersion;
}