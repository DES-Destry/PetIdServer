using System.Diagnostics.CodeAnalysis;

namespace PetIdServer.Core.Users;

public sealed record UserRole : IComparable<UserRole>, IParsable<UserRole>
{
    private const string PetOwnerName = nameof(PetOwner);
    private const string TagCheckerName = nameof(TagChecker);
    private const string TagMasterName = nameof(TagMaster);
    private const string AdminName = nameof(Admin);

    private static readonly IList<UserRole> s_allRoles = [];
    private static readonly Dictionary<string, UserRole> s_rolesByName = new(StringComparer.OrdinalIgnoreCase);

    public static readonly UserRole PetOwner = new(PetOwnerName, 0);
    public static readonly UserRole TagChecker = new(TagCheckerName, 1000);
    public static readonly UserRole TagMaster = new(TagMasterName, 2000);
    public static readonly UserRole Admin = new(AdminName, ushort.MaxValue);

    public static readonly UserRole LeastPrivileged = PetOwner;
    public static readonly UserRole MostPrivileged = Admin;

    private UserRole(string name, ushort level)
    {
        Name = name;
        Level = level;

        s_allRoles.Add(this);
        s_rolesByName.Add(name, this);
    }

    public string Name { get; }
    public ushort Level { get; }

    public static IEnumerable<UserRole> All => s_allRoles;

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
        if (!string.IsNullOrWhiteSpace(s))
        {
            return s_rolesByName.TryGetValue(s.Trim(), out result);
        }

        result = null;
        return false;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out UserRole result) => TryParse(s, null, out result);


    public bool HasPermissionsOf(UserRole role) => Level == role.Level || Level == MostPrivileged.Level;

    public override string ToString() => Name;
}
