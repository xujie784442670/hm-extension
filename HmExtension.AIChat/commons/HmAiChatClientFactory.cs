using HmExtension.AIChat.xinghuo;

namespace HmExtension.AIChat.commons;

public static class HmAiChatClientFactory
{
    /// <summary>
    /// 创建星火客户端
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static HmAiChatClient<XinHuoModel> Create(XingHuoChatOption option)
    {
        return new HmAiChatXingHuoClient(option);
    }
}