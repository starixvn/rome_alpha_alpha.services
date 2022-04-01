namespace Alpha.API.Client.Models.Outstanding;

public record TopOutstanding(IEnumerable<DownlineOutstanding> TopOutstandings, DownlineOutstanding Total, int HiddenDownlinesCount);
