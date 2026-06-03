using System.Globalization;

namespace Toarnbeike.Results.Guards.Messages;

internal static class MessageProviderFactory
{
    public static IFailureMessageProvider GetForGuardFailures(CultureInfo? culture = null)
    {
        Console.WriteLine(culture);
        return new DefaultFailureMessageProvider(culture ?? CultureInfo.InvariantCulture);
    }

    public static IFailureMessageProvider GetForValidationFailures(CultureInfo? culture = null)
    {
        Console.WriteLine(culture);
        return new DefaultFailureMessageProvider(culture ?? CultureInfo.InvariantCulture);
    }
}
