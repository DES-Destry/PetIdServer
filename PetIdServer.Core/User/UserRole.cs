using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Domain.User;

public abstract record UserRole(string Name, ushort Level) : IComparable<UserRole>, IParsable<UserRole>
{
    private const string PetOwnerName = nameof(PetOwner);
    private const string TagCheckerName = nameof(TagChecker);
    private const string AdminName = nameof(Admin);

    private static readonly ImmutableDictionary<string, UserRole> s_rolesByName;

    public static readonly UserRole PetOwner = new PetOwnerRole();
    public static readonly UserRole TagChecker = new TagCheckerRole();
    public static readonly UserRole Admin = new AdminRole();

    public static readonly UserRole LeastPrivileged = PetOwner;
    public static readonly UserRole MostPrivileged = Admin;

    static UserRole()
    {
        s_rolesByName = ImmutableDictionary.CreateRange(StringComparer.OrdinalIgnoreCase, [
            new KeyValuePair<string, UserRole>(PetOwnerName, PetOwner),
            new KeyValuePair<string, UserRole>(TagCheckerName, TagChecker),
            new KeyValuePair<string, UserRole>(AdminName, Admin)
        ]);
    }

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

        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        bool returnValue = s_rolesByName.TryGetValue(s, out UserRole? role);
        result = role;

        return returnValue;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out UserRole result)
    {
        return TryParse(s, null, out result);
    }

    public bool HasPermissionsOf(UserRole role) => Level >= role.Level;

    public override string ToString() => Name;

    private sealed record PetOwnerRole() : UserRole(PetOwnerName, 0);

    private sealed record TagCheckerRole() : UserRole(TagCheckerName, 1000);

    private sealed record AdminRole() : UserRole(AdminName, ushort.MaxValue);
}
