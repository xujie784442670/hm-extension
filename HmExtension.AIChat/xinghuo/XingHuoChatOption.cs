using HmExtension.AIChat.commons;

namespace HmExtension.AIChat.xinghuo;

public class XingHuoChatOption:HmAiChatOption
{
    public string AppId
    {
        get => ContainsKey(nameof(AppId)) ? (string)this[nameof(AppId)] : null;
        set => this[nameof(AppId)] = value;
    }
}