namespace PetIdServer.Core.Tags;

public record TagStatus(string Value)
{
    public static readonly TagStatus Registered = new("Registered");
    public static readonly TagStatus Produced = new("Produced");

    public static readonly TagStatus SentToExternalRetailer = new("SentToExternalRetailer");

    public static readonly TagStatus GoingToStore = new("GoingToStore");
    public static readonly TagStatus InStore = new("InStore");
    public static readonly TagStatus Sold = new("Sold");

    public static readonly TagStatus ClearedByAdmin = new("ClearedByAdmin");
    public static readonly TagStatus InUse = new("InUse");

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
}
