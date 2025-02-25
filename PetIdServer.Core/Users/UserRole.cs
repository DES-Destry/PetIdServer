using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Users;

public sealed record UserRole(string Name, ushort Level) : IComparable<UserRole>, IParsable<UserRole>
{
    private const string PetOwnerName = nameof(PetOwner);
    private const string TagCheckerName = nameof(TagChecker);
    private const string TagMasterName = nameof(TagMaster);
    private const string AdminName = nameof(Admin);

    private static readonly ImmutableDictionary<string, UserRole> s_rolesByName;

    public static readonly UserRole PetOwner = new(PetOwnerName, 0);
    public static readonly UserRole TagChecker = new(TagCheckerName, 1000);
    public static readonly UserRole TagMaster = new(TagMasterName, 2000);
    public static readonly UserRole Admin = new(AdminName, ushort.MaxValue);

    public static readonly UserRole LeastPrivileged = PetOwner;
    public static readonly UserRole MostPrivileged = Admin;

    static UserRole()
    {
        s_rolesByName = All.ToImmutableDictionary(r => r.Name, StringComparer.OrdinalIgnoreCase);
    }

    public static IEnumerable<UserRole> All => [PetOwner, TagChecker, TagMaster, Admin];

    public int CompareTo(UserRole? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is null ? 1 : Level.CompareTo(other.Level);
    }

    public static UserRole Parse(string s, IFormatProvider? provider = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(s);

        if (!s_rolesByName.TryGetValue(s.Trim(), out UserRole? role))
        {
            throw new ArgumentException($"Invalid UserRole value: {s}");
        }

        return role;
    }

    public static bool TryParse([NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out UserRole result)
    {
        result = null;

        return !string.IsNullOrWhiteSpace(s) &&
               s_rolesByName.TryGetValue(s, out result);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out UserRole result) =>
        TryParse(s, null, out result);


    public bool HasPermissionsOf(UserRole role) => Level == role.Level || Level == MostPrivileged.Level;

    public override string ToString() => Name;
}
