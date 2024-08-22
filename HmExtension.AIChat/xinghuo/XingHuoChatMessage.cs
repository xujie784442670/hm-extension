using System.Collections.Generic;
using HmExtension.AIChat.commons;
using Newtonsoft.Json;

namespace HmExtension.AIChat.xinghuo;

public class XingHuoChatMessage
{
    [JsonProperty("role")]
    public string Role { get;}

    [JsonProperty("content")]
    public string Content { get; set; }

    public XingHuoChatMessage(Role role, string content)
    {
        Role = role.Value;
        Content = content;
    }
}

public class Role(string name, string description, string value) :BaseModel<string>(name, description, value)
{
    /// <summary>
    /// 用于设置对话背景
    /// </summary>
    public static readonly Role System = AddModel(nameof(Role), new Role("System", "用于设置对话背景","system"));
    /// <summary>
    /// 表示是用户的问题
    /// </summary>
    public static readonly Role User = AddModel(nameof(Role), new Role("User", "表示是用户的问题", "user"));
    /// <summary>
    /// 表示AI的回复
    /// </summary>
    public static readonly Role Assistant = AddModel(nameof(Role), new Role("Assistant", "表示AI的回复", "assistant"));
    public override List<IModel<string>> GetModels()
    {
        return GetModels(nameof(Role));
    }

    public override IModel<string> ValueOf(string name)
    {
        return Role.ValueOf(typeof(Role), name);
    }
}