using Toarnbeike.Results.Ensure.PrimitiveExtensions;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Ensure.Tests.Example;

public sealed record Personalia
{
    public Naam Naam { get; }
    public Geslacht Geslacht { get; }
    public DateOnly Geboortedatum { get; }
    public int LidSinds { get; }

    public static Result<Personalia> Create(Naam naam, Geslacht geslacht, DateOnly geboortedatum, int lidSinds)
    {
        return Result
            .Ensure(() => geslacht.IsDefined())
            .Ensure(() => geboortedatum.After(new DateOnly(1900,1,1)))
            .Ensure(() => geboortedatum.Before(DateOnly.FromDateTime(DateTime.Today)))
            .Ensure(() => lidSinds.InRange(1990, DateTime.Now.Year))
            .Ensure(() => VerifyGeboortedatumAgainstLidSinds(geboortedatum, lidSinds))
            .WithValue(() => new Personalia(naam, geslacht, geboortedatum, lidSinds));
    }


    private static Result<int> VerifyGeboortedatumAgainstLidSinds(DateOnly geboortedatum, int lidSinds)
    {
        return geboortedatum.Year > lidSinds
            ? new SimpleFailure("LidSinds.Invalid",
                "Lid sinds kan niet eerder zijn dan de opgegeven geboortedatum van het personalia.")
            : Result.Success(lidSinds);
    }

    private Personalia(Naam naam, Geslacht geslacht, DateOnly geboortedatum, int lidSinds)
    {
        Naam = naam;
        Geslacht = geslacht;
        Geboortedatum = geboortedatum;
        LidSinds = lidSinds;
    }
}

public sealed record Naam(string Voorletters, string Achternaam);

public enum Geslacht
{
    Man, 
    Vrouw,
    Anders
}