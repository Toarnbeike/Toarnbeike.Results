using System.Globalization;

namespace Toarnbeike.Results.Ensure.Tests;

public abstract class InvariantCultureTestBase
{
    private CultureInfo? _originalCulture;
    private CultureInfo? _originalUiCulture;
    private CultureInfo? _originalDefaultThreadCulture;
    private CultureInfo? _originalUiDefaultThreadCulture;

    [Before(Test)]
    public void SetCulture()
    {
        _originalCulture = CultureInfo.CurrentCulture;
        _originalUiCulture = CultureInfo.CurrentUICulture;
        _originalDefaultThreadCulture = CultureInfo.DefaultThreadCurrentCulture;
        _originalUiDefaultThreadCulture = CultureInfo.DefaultThreadCurrentUICulture;

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
    }

    [After(Test)]
    public void RestoreCulture()
    {
        CultureInfo.DefaultThreadCurrentCulture = _originalDefaultThreadCulture;
        CultureInfo.DefaultThreadCurrentUICulture = _originalUiDefaultThreadCulture;
        CultureInfo.CurrentCulture = _originalCulture ?? CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = _originalUiCulture ?? CultureInfo.InvariantCulture;
    }
}