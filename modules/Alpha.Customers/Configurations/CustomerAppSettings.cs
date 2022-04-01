namespace Alpha.Customers.Configurations;

public sealed class CustomerAppSettings
{
    public int CustomerInfoCacheInSeconds { get; init; }

    public int CustomerStatusCacheInSeconds { get; init; }

    public int SubAccPermissionCacheInSeconds { get; init; }
}
