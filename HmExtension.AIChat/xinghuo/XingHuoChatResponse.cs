using System.Collections.Generic;
using Newtonsoft.Json;

namespace HmExtension.AIChat.xinghuo;

public class XingHuoChatResponse
{
    [JsonProperty("code")] public int Code { get; set; }

    [JsonProperty("message")] public string Message { get; set; }

    [JsonProperty("sid")] public string Sid { get; set; }

    [JsonProperty("usage")] public Dictionary<string, int> Usage { get; set; } = new();

    public int PromptTokens => Usage["prompt_tokens"];

    public int CompletionTokens => Usage["completion_tokens"];

    public int TotalTokens => Usage["total_tokens"];

    [JsonProperty("choices")] public List<MessageResponse> Choices { get; } = new();
    /*
    {
    "code":0,
    "message":"Success",
    "sid":"cha000be930@dx1917406ababb8f2532",
    "choices":[
        {
            "message":{
                "role":"assistant",
                "content":"好的，这是一个冷笑话：\n\n为什么鱼不会唱歌？因为它们有鳞片。"
            },
            "index":0
        }]
    ,"usage":{
        "prompt_tokens":13,
        "completion_tokens":20,
        "total_tokens":33}
    }
    */
}

public class MessageResponse
{
    [JsonProperty("message")] public Dictionary<string, string> Message { get; set; } = new();
    [JsonProperty("index")] public int Index { get; set; }

    public XingHuoChatMessage ToXingHuoChatMessage()
    {
        return new((Role)Role.ValueOf(typeof(Role),Message["role"]), Message["content"]);
    }
}