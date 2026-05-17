using System.Globalization;

namespace Toarnbeike.Results.Ensure.Tests;

public class InvariantCultureTestBase
{
    private static CultureInfo? _originalCulture;
    private static CultureInfo? _originalUiCulture;

    [Before(TestSession)]
    public static void SetInvariantCulture()
    {
        _originalCulture = CultureInfo.CurrentCulture;
        _originalUiCulture = CultureInfo.CurrentUICulture;

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
    }

    [After(TestSession)]
    public static void RestoreOriginalCulture()
    {
        CultureInfo.CurrentCulture = _originalCulture!;
        CultureInfo.CurrentUICulture = _originalUiCulture!;
        CultureInfo.DefaultThreadCurrentCulture = _originalCulture!;
        CultureInfo.DefaultThreadCurrentUICulture = _originalUiCulture!;
    }
}