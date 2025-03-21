using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Tags;

[DebuggerDisplay("{Value}")]
public sealed record TagStatus : IParsable<TagStatus>
{
    private static readonly Dictionary<string, TagStatus> s_tagStatusesByName = new(StringComparer.OrdinalIgnoreCase);

    public static readonly TagStatus Registered = new("Registered");
    public static readonly TagStatus Produced = new("Produced");

    public static readonly TagStatus SentToExternalRetailer = new("SentToExternalRetailer");

    public static readonly TagStatus GoingToStore = new("GoingToStore");
    public static readonly TagStatus InStore = new("InStore");
    public static readonly TagStatus Sold = new("Sold");

    public static readonly TagStatus ClearedByAdmin = new("ClearedByAdmin");
    public static readonly TagStatus InUse = new("InUse");

    public static readonly TagStatus Destroyed = new("Destroyed");
    public static readonly TagStatus Unknown = new("Unknown");

    private TagStatus(string value)
    {
        Value = value;
        s_tagStatusesByName.Add(value, this);
    }

    public string Value { get; init; }

    public static TagStatus Initial => Registered;

    public static IEnumerable<TagStatus> ReadyToSell => [InStore];
    public static IEnumerable<TagStatus> ReadyToUse => [SentToExternalRetailer, Sold];

    public static TagStatus Parse(string s, IFormatProvider? provider = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(s);

        if (!s_tagStatusesByName.TryGetValue(s.Trim(), out TagStatus? result))
        {
            throw new ArgumentException($"Unknown tag status: {s}", nameof(s));
        }

        return result;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out TagStatus result)
    {
        if (!string.IsNullOrWhiteSpace(s))
        {
            return s_tagStatusesByName.TryGetValue(s.Trim(), out result);
        }

        result = null;
        return false;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out TagStatus result) => TryParse(s, null, out result);

    public override string ToString() => Value;
}
