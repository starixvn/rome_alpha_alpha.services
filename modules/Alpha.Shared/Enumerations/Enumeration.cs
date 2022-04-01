using System.Reflection;

namespace Alpha.Shared.Enumerations;

public abstract class Enumeration : IComparable
{
    protected Enumeration()
    {
    }

    protected Enumeration(byte value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    public byte Value { get; }

    public string DisplayName { get; } = default!;

    public override string ToString() => DisplayName;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
        => typeof(T)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
        .Select(f => f.GetValue(null))
        .Cast<T>();

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        => Math.Abs(firstValue.Value - secondValue.Value);

    public static T FromValue<T>(byte value) where T : Enumeration
        => Parse<T, byte>(value, "value", item => item.Value == value);

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);
        ArgumentNullException.ThrowIfNull(matchingItem, nameof(value));

        return matchingItem;
    }

    public int CompareTo(object? other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return Value.CompareTo(((Enumeration)other).Value);
    }

    public static bool operator ==(Enumeration left, Enumeration right)
        => left.Value == right.Value;

    public static bool operator !=(Enumeration left, Enumeration right)
        => !(left == right);
}
