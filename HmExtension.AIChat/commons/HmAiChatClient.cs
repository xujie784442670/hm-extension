using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HmExtension.AIChat.commons;

public abstract class HmAiChatClient<M>(HmAiChatOption option)
{
    public readonly HmAiChatOption Option = option;


    public void UpdateOption<TO>(Action<TO> action) where TO : HmAiChatOption
    {
        action?.Invoke((TO)Option);
    }

    /// <summary>
    /// 设置授权信息
    /// </summary>
    /// <param name="httpClient"></param>
    public abstract void SetAuthorization(HttpClient httpClient);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public abstract void SetSystemMessage(string message);

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="request"></param>
    /// <param name="completionOption"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> SendRequest(HttpRequestMessage request,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken? cancellationToken = null)
    {
        var httpClient = new HttpClient();
        SetAuthorization(httpClient);
        return httpClient.SendAsync(request, completionOption, cancellationToken ?? CancellationToken.None);
    }

    public virtual bool CheckOptionValidity(out string errorMessage)
    {
        errorMessage = null;
        if (string.IsNullOrWhiteSpace(Option.ApiUrl))
        {
            errorMessage = "API服务地址不能为空";
            return false;
        }

        if (Option.ApiSecret == null)
        {
            errorMessage = "API密钥不能为空";
            return false;
        }

        return true;
    }
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="model"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public abstract Task<HttpResponseMessage> SendRequestAsync(M model, string message);

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="model"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public abstract Task<string> SendMessageAsync(M model,string message);

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="model"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    public abstract Task<Stream> SendStreamAsync(M model, Stream stream);
}