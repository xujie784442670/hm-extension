using System.Collections.Generic;

namespace HmExtension.AIChat.commons;

public abstract class HmAiChatOption : Dictionary<string, object>
{
    public string ApiUrl
    {
        get => ContainsKey(nameof(ApiUrl)) ? $"{this[nameof(ApiUrl)]}" : null;
        set => this[nameof(ApiUrl)] = value;
    }

    public string ApiSecret
    {
        get => ContainsKey(nameof(ApiSecret)) ? $"{this[nameof(ApiSecret)]}" : null;
        set => this[nameof(ApiSecret)] = value;
    }
}