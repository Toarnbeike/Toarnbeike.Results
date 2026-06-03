using System.Globalization;

namespace Toarnbeike.Results.Ensure.Tests;

[NotInParallel]
public abstract class InvariantCultureTestBase
{
    private readonly CultureInfo _invariant = CultureInfo.InvariantCulture;
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

        CultureInfo.DefaultThreadCurrentCulture = _invariant;
        CultureInfo.DefaultThreadCurrentUICulture = _invariant;
        CultureInfo.CurrentCulture = _invariant;
        CultureInfo.CurrentUICulture = _invariant;
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