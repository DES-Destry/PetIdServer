using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Tags;

[DebuggerDisplay("{Value}")]
public sealed record TagFeature(string Value) : IParsable<TagFeature>
{
    private static readonly ImmutableDictionary<string, TagFeature> s_tagFeaturesByName =
        All.ToImmutableDictionary(f => f.Value, StringComparer.OrdinalIgnoreCase);

    public static TagFeature Qr => new("Qr");
    public static TagFeature Nfc => new("Nfc");
    public static TagFeature Rfid => new("Rfid");
    public static TagFeature Bluetooth => new("Bluetooth");
    public static TagFeature Gps => new("Gps");

    public static IEnumerable<TagFeature> DefaultSetOfFeatures => [Qr];
    public static IEnumerable<TagFeature> All => [Qr, Nfc, Rfid, Bluetooth, Gps];


    public static TagFeature Parse(string s, IFormatProvider? provider = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(s);

        if (!s_tagFeaturesByName.TryGetValue(s.Trim(), out TagFeature? feature))
        {
            throw new ArgumentException($"Invalid TagFeature value: {s}");
        }

        return feature;
    }

    public static bool TryParse([NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out TagFeature result)
    {
        if (!string.IsNullOrWhiteSpace(s))
        {
            return s_tagFeaturesByName.TryGetValue(s.Trim(), out result);
        }

        result = null;
        return false;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out TagFeature result) => TryParse(s, null, out result);

    public override string ToString() => Value;
}
