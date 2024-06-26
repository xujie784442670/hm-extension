using System;
using Aop.Api;
using Aop.Api.Request;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HmExtension.Pay;

/// <summary>
/// 支付宝全局上下文
/// </summary>
public class AlipayContext
{
    private static IAopClient _client;

    /// <summary>
    /// 初始化支付宝全局上下文
    /// </summary>
    /// <param name="serverUrl">网关地址</param>
    /// <param name="appId">AppID</param>
    /// <param name="privateKey">应用私钥</param>
    /// <param name="alipayPublicKey">支付宝公钥</param>
    /// <param name="format">数据格式,默认: json</param>
    /// <param name="signType">签名类型,默认: RSA2</param>
    /// <param name="charset">字符集编码</param>
    /// <param name="keyFromFile">密钥是否来自文件,如果为true,则privateKey和alipayPublicKey参数传递密钥文件路径</param>
    public static void Init(
        string serverUrl,
        string appId,
        string privateKey,
        string alipayPublicKey,
        string format = "json",
        string signType = "RSA2",
        string charset = "utf-8",
        bool keyFromFile = false)
    {
        AlipayConfig config = new AlipayConfig
        {
            ServerUrl = serverUrl,
            AppId = appId,
            PrivateKey = privateKey,
            Format = format,
            SignType = signType,
            AlipayPublicKey = alipayPublicKey,
            Charset = charset
        };
        if (keyFromFile)
        {
            config.PrivateKey = string.IsNullOrWhiteSpace(privateKey) ? "" : System.IO.File.ReadAllText(privateKey);
            config.AlipayPublicKey = string.IsNullOrWhiteSpace(alipayPublicKey)
                ? ""
                : System.IO.File.ReadAllText(alipayPublicKey);
        }

        Init(config);
    }

    /// <summary>
    /// 初始化支付宝全局上下文
    /// </summary>
    /// <param name="config">支付宝配置对象</param>
    public static void Init(AlipayConfig config)
    {
        _client = new DefaultAopClient(config);
    }

    /// <summary>
    /// 执行支付宝请求
    /// </summary>
    /// <typeparam name="T">请求类型</typeparam>
    /// <param name="request">请求对象</param>
    /// <returns>响应对象</returns>
    /// <exception cref="Exception"></exception>
    public static T Execute<T>(IAopRequest<T> request) where T : AopResponse
    {
        if (_client == null)
        {
            throw new Exception("请先调用[HmExtension.Pay.AlipayContext.Init]方法,初始化支付宝全局上下文");
        }

        return _client.Execute(request);
    }

    /// <summary>
    /// 异常检查,用于检查支付宝响应是否有错误,如果有错误则抛出异常
    /// </summary>
    /// <typeparam name="T">响应类型</typeparam>
    /// <param name="response">响应对象</param>
    /// <exception cref="Exception"></exception>
    public static void Execption<T>(T response) where T : AopResponse
    {
        if (response.IsError)
        {
            throw new Exception($"调用失败[{response.Code}-{response.SubCode}] {response.Msg}:{response.SubMsg}");
        }
        
    }
}