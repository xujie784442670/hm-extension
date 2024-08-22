using System.Collections.Generic;
using System.IO;
using System.Linq;
using HmExtension.AIChat.commons;
using Newtonsoft.Json;

namespace HmExtension.AIChat.xinghuo;
/// <summary>
/// 星火模型请求
/// </summary>
public class XinHuoChatRequest
{
    public enum ToolChoiceType
    {
        /// <summary>
        /// 不调用
        /// </summary>
        None,
        /// <summary>
        /// 自动判断
        /// </summary>
        Auto
    }

    /// <summary>
    /// 选择请求的模型版本
    /// general指向Lite版本；
    /// generalv3指向Pro版本；
    /// pro-128k指向Pro-128K版本；
    /// generalv3.5指向Max版本；
    /// 4.0Ultra指向4.0 Ultra版本；
    /// </summary>
    public XinHuoModel Model { get; set; }
    public List<XingHuoChatMessage> Messages { get; } = new();
    /// <summary>
    /// 是否开启流式传输
    /// </summary>
    public bool? Stream {get; set; }
    /// <summary>
    /// 核采样阈值。用于决定结果随机性，取值越高随机性越强即相同的问题得到的不同答案的可能性越高,取值范围 [0,2] ，默认值1
    /// </summary>
    public double? Temperature { get; set; } = 1;
    /// <summary>
    /// 可供模型调用的工具
    /// </summary>
    public List<string> Tools { get; } = new();
    /// <summary>
    /// 用于控制模型是如何选择要调用的函数
    /// </summary>
    public ToolChoiceType? ToolChoice { get; set; }
    /// <summary>
    /// 模型回答的tokens的最大长度
    /// <para>
    /// Pro、Max、4.0 Ultra 取值为[1,8192]，默认为4096;
    /// Lite、Pro-128K 取值为[1,4096]，默认为4096。
    /// </para>
    /// </summary>
    public int? MaxTokens;
    /// <summary>
    /// 从k个候选中随机选择⼀个（⾮等概率）
    /// <para>
    /// 取值为[1,6],默认为4
    /// </para>
    /// </summary>
    public int? TopK;

    public XinHuoChatRequest():this(XinHuoModel.Lite)
    {
    }

    public XinHuoChatRequest(XinHuoModel model)
    {
        Model = model;
    }
    public XinHuoChatRequest(XinHuoModel model, List<XingHuoChatMessage> messages)
    {
        Model = model;
        if (messages != null)
        {
            Messages.AddRange(messages);
        }
    }

    /// <summary>
    /// 添加消息
    /// </summary>
    /// <param name="role"></param>
    /// <param name="content"></param>
    public void AddMessage(Role role, string content)
    {
        Messages.Add(new XingHuoChatMessage(role, content));
    }

    public string ToJson()
    {
        var dict = new Dictionary<string, object>
        {
            {"messages", Messages},
            { "model", Model.Value }
        };
        if(Stream != null) dict.Add("stream",Stream);
        if(Temperature != null) dict.Add("temperature", Temperature);
        if(Tools != null) dict.Add("tools", Tools);
        if(ToolChoice != null) dict.Add("tool_choice", ToolChoice.ToString().ToLower());
        if(MaxTokens != null) dict.Add("max_tokens", MaxTokens);
        if(TopK != null) dict.Add("top_k", TopK);
        return JsonConvert.SerializeObject(dict);
    }
}

public class XinHuoModel(string name, string description, string value) : BaseModel<string>(name, description, value)
{
    /// <summary>
    /// general指向Lite版本
    /// </summary>
    public static readonly XinHuoModel Lite = AddModel(nameof(XinHuoModel),new XinHuoModel("Lite", "general指向Lite版本", "general"));
    /// <summary>
    /// generalv3指向Pro版本；
    /// </summary>
    public static readonly XinHuoModel Pro = AddModel(nameof(XinHuoModel), new XinHuoModel("Pro", "generalv3指向Pro版本", "generalv3"));

    /// <summary>
    /// pro-128k指向Pro-128K版本；
    /// </summary>
    public static readonly XinHuoModel Pro128K = AddModel(nameof(XinHuoModel), new XinHuoModel("Pro128K", "pro-128k指向Pro-128K版本", "pro-128k"));

    /// <summary>
    /// generalv3.5指向Max版本；
    /// </summary>
    public static readonly XinHuoModel Max = AddModel(nameof(XinHuoModel), new XinHuoModel("Max", "generalv3.5指向Max版本", "generalv3.5"));

    /// <summary>
    /// 4.0Ultra指向4.0 Ultra版本；
    /// </summary>
    public static readonly XinHuoModel Ultra = AddModel(nameof(XinHuoModel), new XinHuoModel("Ultra", "4.0Ultra指向4.0 Ultra版本", "4.0Ultra"));


    public override List<IModel<string>> GetModels()
    {
        return GetModels(nameof(XinHuoModel));
    }

    public override IModel<string> ValueOf(string name)
    {
        return ValueOf(typeof(XinHuoModel), name);
    }
}