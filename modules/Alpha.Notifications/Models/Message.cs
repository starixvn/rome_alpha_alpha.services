namespace Alpha.Notifications.Models;

public record Message
{
    public Message()
    {
    }

    public Message(
        int messageId,
        byte msgType,
        string contentEN,
        string contentCS,
        string contentCH,
        string contentTH,
        string contentVI,
        int targetId,
        string betTransId)
    {
        MessageId = messageId;
        TargetId = targetId;
        MsgType = msgType;
        ContentEN = contentEN;
        ContentCS = contentCS;
        ContentCH = contentCH;
        ContentTH = contentTH;
        ContentVI = contentVI;
        BetTransId = betTransId;
    }

    public int MessageId { get; }

    public byte MsgType { get; }

    public string ContentEN { get; } = default!;

    public string ContentCS { get; } = default!;

    public string ContentCH { get; } = default!;

    public string ContentTH { get; } = default!;

    public string ContentVI { get; } = default!;

    public int TargetId { get; }

    public string BetTransId { get; } = default!;

    // public IDictionary<string, string> Contents
    //     => new Dictionary<string, string>
    //     {
    //         [LanguageConstants.English] = ContentEN ?? string.Empty,
    //         [LanguageConstants.ChineseSimplified] = ContentCS ?? string.Empty,
    //         [LanguageConstants.ChineseTraditional] = ContentCH ?? string.Empty,
    //         [LanguageConstants.Japanese] = ContentJP ?? string.Empty,
    //         [LanguageConstants.Thai] = ContentTH ?? string.Empty,
    //         [LanguageConstants.Korean] = ContentKO ?? string.Empty,
    //         [LanguageConstants.Vietnamese] = ContentVI ?? string.Empty
    //     };

    // public string GetContentByLanguage(string languageKey)
    //     => Contents.ContainsKey(languageKey) && !string.IsNullOrEmpty(Contents[languageKey])
    //         ? Contents[languageKey]
    //         : Contents[LanguageConstants.English];
}
