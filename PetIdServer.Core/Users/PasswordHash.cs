using System.Diagnostics.CodeAnalysis;
using PetIdServer.Core.Common;

namespace PetIdServer.Core.Users;

public class PasswordHash : ValueObject
{
    private PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Password hash cannot be empty", nameof(value));
        }

        if (!IsValidHash(value))
        {
            throw new ArgumentException("Invalid password hash format.", nameof(value));
        }

        Value = value;
    }

    private string Value { get; }

    [return: NotNullIfNotNull("hash")]
    public static implicit operator string?(PasswordHash? hash) => hash?.Value;

    [return: NotNullIfNotNull("value")]
    public static PasswordHash? FromHash(string? value) => value is null ? null : new PasswordHash(value);

    public static bool IsValidHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Password hash cannot be empty", nameof(value));
        }

        string[] parts = value.Split('-');

        if (parts.Length != 2)
        {
            return false;
        }

        return IsHex(parts[0]) && IsHex(parts[1]);
    }

    private static bool IsHex(string value) =>
        value.All(character => char.IsDigit(character) || character >= 'a' || character <= 'f');


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return nameof(PasswordHash);
        yield return nameof(Value);
        yield return Value;
    }
}
