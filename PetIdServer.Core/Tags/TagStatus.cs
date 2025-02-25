using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Tags;

public sealed record TagStatus(string Value) : IParsable<TagStatus>
{
    public static readonly TagStatus Registered = new("Registered");
    public static readonly TagStatus Produced = new("Produced");

    public static readonly TagStatus SentToExternalRetailer = new("SentToExternalRetailer");

    public static readonly TagStatus GoingToStore = new("GoingToStore");
    public static readonly TagStatus InStore = new("InStore");
    public static readonly TagStatus Sold = new("Sold");

    public static readonly TagStatus ClearedByAdmin = new("ClearedByAdmin");
    public static readonly TagStatus InUse = new("InUse");

    private static readonly ImmutableDictionary<string, TagStatus> s_tagStatusesByName =
        All.ToImmutableDictionary(status => status.Value, StringComparer.OrdinalIgnoreCase);

    public static TagStatus Destroyed => new("Destroyed");
    public static TagStatus Unknown => new("Unknown");

    public static TagStatus Initial => Registered;

    public static IEnumerable<TagStatus> ReadyToSell => [InStore];
    public static IEnumerable<TagStatus> ReadyToUse => [SentToExternalRetailer, Sold];

    public static IEnumerable<TagStatus> All =>
    [
        Registered,
        Produced,
        SentToExternalRetailer,
        GoingToStore,
        InStore,
        Sold,
        InUse,
        Destroyed,
        Unknown
    ];

    public static TagStatus Parse(string s, IFormatProvider? provider)
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
}
