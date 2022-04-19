namespace Alpha.Shared.Enumerations;

public sealed class UserLevel : Enumeration
{
    public static readonly UserLevel Super = new UserLevel(4, "Super");
    public static readonly UserLevel Master = new UserLevel(3, "Master");
    public static readonly UserLevel Agent = new UserLevel(2, "Agent");
    public static readonly UserLevel Member = new UserLevel(1, "Member");

    public UserLevel()
    {
    }

    private UserLevel(byte value, string displayName)
        : base(value, displayName)
    {
    }

    public bool IsSuper() => this == Super;

    public bool IsMaster() => this == Master;

    public bool IsAgent() => this == Agent;

    public UserLevel GetDirectDownlineLevel() => FromValue<UserLevel>((byte)(Value - 1));
}
