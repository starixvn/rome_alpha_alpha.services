using Fanex.RemotingRef;

namespace Alpha.Shared.Services;

public interface ICacheRefService
{
    string GetProductName(int productId, string languageCode, bool belonging777Group);

    string GetResourceDataByRefIdAndResourceId(string refId, string resourceId, string languageCode);
}

public class CacheRefService : ICacheRefService
{
    private readonly IRemotingRefService remotingRefService;

    public CacheRefService(IRemotingRefService remotingRefService)
    {
        this.remotingRefService = remotingRefService;
    }

    public string GetProductName(int productId, string languageCode, bool belonging777Group)
    {
        if(belonging777Group)
        {
            var name = GetResourceDataByRefIdAndResourceId("Product_Saba", productId.ToString(), languageCode);
            return string.IsNullOrEmpty(name) ? GetResourceDataByRefIdAndResourceId("Product", productId.ToString(), languageCode) : name;
        }

        return GetResourceDataByRefIdAndResourceId("Product", productId.ToString(), languageCode);
    }
        

    public string GetResourceDataByRefIdAndResourceId(string refId, string resourceId, string languageCode)
        => remotingRefService.GetResourceDataByRefIdAndResourceId(
            refId,
            resourceId,
            languageCode,
            resourceId);
}
