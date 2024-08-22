using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HmExtension.AIChat.commons;
using HmExtension.Commons.Extensions;
using Newtonsoft.Json;
using StringContent = System.Net.Http.StringContent;

namespace HmExtension.AIChat.xinghuo;

public class HmAiChatXingHuoClient(XingHuoChatOption option) :HmAiChatClient<XinHuoModel>(option)
{
    private readonly List<XingHuoChatMessage> histories = new();

    public override void SetAuthorization(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{((XingHuoChatOption)Option).AppId}:{Option.ApiSecret}");
    }

    public override void SetSystemMessage(string message)
    {
        histories.Add(new XingHuoChatMessage(Role.System, message));
    }

    public override Task<HttpResponseMessage> SendRequestAsync(XinHuoModel model, string message)
    {
        var req = new XinHuoChatRequest(model);
        req.Messages.AddRange(histories);
        req.Messages.Add(new XingHuoChatMessage(Role.User,message));
        var json = req.ToJson();
        json.Println("JSON: ");
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return SendRequest(new HttpRequestMessage(HttpMethod.Post, Option.ApiUrl)
        {
            Content = content
        });
    }

    public override async Task<string> SendMessageAsync(XinHuoModel model, string message)
    {
        var resp = await SendRequestAsync(model,message);
        if (!resp.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"请求失败，状态码：{resp.ReasonPhrase}");
        }

        var msg =await resp.Content.ReadAsStringAsync();
        var respMsg = msg.FromJson<XingHuoChatResponse>();
        Console.WriteLine(respMsg.ToJson());
        var xingHuoChatMessage = respMsg.Choices[0].ToXingHuoChatMessage();
        histories.Add(xingHuoChatMessage);
        return xingHuoChatMessage.Content;
    }

    public override Task<Stream> SendStreamAsync(XinHuoModel model, Stream stream)
    {
        throw new System.NotImplementedException();
    }
}