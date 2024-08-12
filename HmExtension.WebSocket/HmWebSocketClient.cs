using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.Remoting.Messaging;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using TouchSocket.Core;
using TouchSocket.Http;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace HmExtension.WebSocket;

public class HmWebSocketClient : ReceivedAdapter
{
    public event Action<bool> OnChangedConnected;

    public string Name => $"{Id}:{IP}:{Port}";

    public string Id { get; }

    public string IP => WebSocket.Client.IP;

    public int Port => WebSocket.Client.Port;

    public bool IsPost { get; private set; }

    public IWebSocket WebSocket { get; private set; }

    public HttpContext HttpContext { get; internal set; }

    /// <summary>
    /// 业务数据对象
    /// </summary>
    public object BusinessData { get; set; }

    private readonly Dictionary<string, object> _properties = new();

    private readonly Dictionary<string, string> _headers = new();

    private TouchSocketConfig config = new();

    private WebSocketAdapter adapter = new();

    public bool IsConsoleLogger { get; set; } = true;
    /// <summary>
    /// 是否断线重连
    /// </summary>
    public bool IsReconnection { get; set; } = false;

    private bool _isConnected;

    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            if (_isConnected != value)
            {
                _isConnected = value;
                OnChangedConnected?.Invoke(value);
            }
        }
    }


    private string _url;

    public string Url
    {
        get => _url;
        set
        {
            _url = value;
            config.SetRemoteIPHost(value);
        }
    }

    /// <summary>
    /// 创建WebSocket客户端
    /// </summary>
    /// <param name="url">连接URL(ws://或wss://)</param>
    /// <param name="isPost">是否使用Post请求方式</param>
    /// <param name="certificatePath">认证文件路径(wss必须)</param>
    /// <param name="password">密码(wss必须)</param>
    /// <param name="protocols">协议(wss必须)</param>
    /// <param name="targetHost">目标主机(wss必须)</param>
    /// <param name="validationCallback">验证失败回调(wss可选)</param>
    public HmWebSocketClient(string url = null, bool isPost = false, string certificatePath = null,
        string password = null, SslProtocols protocols = SslProtocols.Tls12, string targetHost = null,
        RemoteCertificateValidationCallback validationCallback = null)
    {
        Client = this;
        IsPost = isPost;
        adapter.Client = this;
        adapter.OnConnected += (client, args) => IsConnected =true;
        
        config.ConfigureContainer(a =>
            {
                if (IsConsoleLogger)
                {
                    a.AddConsoleLogger();
                }
            })
            .ConfigurePlugins(a =>
            {
                if (IsReconnection)
                {
                    a.UseReconnection();
                }

                a.UseWebSocketHeartbeat();
                a.Add(nameof(IWebSocketHandshakingPlugin.OnWebSocketHandshaking),
                    async (IWebSocket client, HttpContextEventArgs e) =>
                    {
                        HttpContext = e.Context;
                        foreach (var kv in _headers)
                        {
                            e.Context.Request.Headers[kv.Key] = kv.Value;
                        }

                        if (IsPost)
                        {
                            e.Context.Request.Method = HttpMethod.Post;
                        }

                        await e.InvokeNext();
                    });
                a.Add(adapter);
            });
        if (!string.IsNullOrWhiteSpace(url))
        {
            config.SetRemoteIPHost(url);
        }
        
        if (!string.IsNullOrWhiteSpace(certificatePath))
        {
            var clientSslOption = new ClientSslOption
            {
                ClientCertificates = [new X509Certificate2(certificatePath, password)],
                SslProtocols = protocols,
                TargetHost = targetHost,
                CertificateValidationCallback =
                    validationCallback ?? ((sender, certificate, chain, sslPolicyErrors) => true)
            };
            config.SetClientSslOption(clientSslOption);
        }
    }

    public HmWebSocketClient(IWebSocket webSocket)
    {
        WebSocket = webSocket ?? throw new ArgumentNullException(nameof(webSocket));
        if (WebSocket is ISocketClient httpSocketClient)
        {
            Id = httpSocketClient.Id;
        }
    }

    /// <summary>
    /// 连接
    /// </summary>
    /// <param name="isPost"></param>
    /// <param name="millisecondsTimeout"></param>
    public void Connect(bool isPost=false,int millisecondsTimeout = 5000)
    {
        if(IsConnected) DisConnected("重复连接,自动断开");
        IsPost = isPost;
        var client = new WebSocketClient();
        client.Setup(config);
        client.Received += OnWebSocketReceived;
        client.Connect(millisecondsTimeout);
        this.WebSocket = client;
    }


    /// <summary>
    /// 断开连接
    /// </summary>
    /// <param name="msg"></param>
    public void DisConnected(string msg)
    {
        if(WebSocket is WebSocketClient client)
        {
            client.Send(new WSDataFrame()
            {
                Opcode = WSDataType.Close
            });
            client.SafeClose(msg);
            IsConnected = false;
        }
    }

    public void AddHeader(string key, string value)
    {
        _headers[key] = value;
    }

    public void RemoveHeader(string key)
    {
        _headers.Remove(key);
    }

    protected override void OnMessageHandler(HmWebSocketClient client, WSDataFrameEventArgs e)
    {
        if (client.Id == this.Id)
        {
            base.OnMessageHandler(client, e);
        }
    }

    public void SetProperty(string key, object value)
    {
        _properties[key] = value;
    }

    public object GetProperty(string key)
    {
        return _properties.TryGetValue(key, out var value) ? value : null;
    }

    public void RemoveProperty(string key)
    {
        _properties.Remove(key);
    }

    public void ClearProperties()
    {
        _properties.Clear();
    }

    /// <summary>
    /// 发送文本
    /// </summary>
    /// <param name="text"></param>
    /// <param name="endOfMessage"></param>
    public void Send(string text, bool endOfMessage = true)
    {
        WebSocket.SendAsync(text, endOfMessage);
    }

    /// <summary>
    /// 发送二进制
    /// </summary>
    /// <param name="data"></param>
    /// <param name="endOfMessage"></param>
    public void Send(byte[] data, bool endOfMessage = true)
    {
        WebSocket.SendAsync(data, endOfMessage);
    }

    /// <summary>
    /// 发送二进制
    /// </summary>
    /// <param name="data"></param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    /// <param name="endOfMessage"></param>
    public void Send(byte[] data, int offset, int length, bool endOfMessage = true)
    {
        WebSocket.SendAsync(data, offset, length, endOfMessage);
    }

    /// <summary>
    /// 发送数据帧
    /// </summary>
    /// <param name="data"></param>
    public void Send(WSDataFrame data)
    {
        WebSocket.SendAsync(data);
    }
}