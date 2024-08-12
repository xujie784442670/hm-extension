using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using TouchSocket.Core;
using TouchSocket.Http;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace HmExtension.WebSocket;

public class WebSocketServer
{
    public bool IsConsoleLogger { get; set; } = true;

    internal Dictionary<IWebSocket, HmWebSocketClient> _clients = new();

    public List<ReceivedAdapter> ReceivedPlugins { get; } = [];

    public string Url { get; set; }

    public int Port { get; set; }

    private bool _isRunning = false;

    public WebSocketAdapter PluginAdapter { get; private set; }


    /// <summary>
    /// 是否正在运行
    /// </summary>
    public bool IsRunning
    {
        get => _isRunning;
        set
        {
            if (_isRunning != value)
            {
                _isRunning = value;
                OnConnectedChanged?.Invoke(value);
            }
        }
    }


    public HttpService _service;

    public event Action<bool> OnConnectedChanged;

    public event Action<HmWebSocketClient, HttpContextEventArgs> OnPreConnected;

    public event Action<HmWebSocketClient, HttpContextEventArgs> OnConnected;

    public event Action<HmWebSocketClient, WSDataFrameEventArgs> OnReceived;

    public event Action<HmWebSocketClient, MsgPermitEventArgs> OnClosing;

    /// <summary>
    /// 连接验证逻辑
    /// </summary>
    public Func<IHttpSocketClient, HttpContext, bool> VerifyHandler { get; set; } = (client, context) => true;

    /// <summary>
    /// 创建一个WebSocket服务器
    /// </summary>
    /// <param name="url">连接URL</param>
    /// <param name="port">端口</param>
    public WebSocketServer(string url = null, int port = 7896)
    {
        Url = url;
        Port = port;
    }

    internal HmWebSocketClient GetClient(IWebSocket client)
    {
        if (!_clients.ContainsKey(client))
        {
            _clients[client] = new HmWebSocketClient(client);
        }
        return _clients[client];
    }


    /// <summary>
    /// 添加消息监听插件
    /// </summary>
    /// <param name="plugin"></param>
    public void Listener(ReceivedAdapter plugin)
    {
        plugin.Server = this;
        if (_service == null)
        {
            ReceivedPlugins.Add(plugin);
        }
        else
        {
            _service.PluginManager.Add(plugin);
        }
    }

    /// <summary>
    /// 容器注册配置
    /// </summary>
    /// <param name="config"></param>
    public virtual void RegistratorConfig(IRegistrator config)
    {
        if (IsConsoleLogger)
        {
            config.AddConsoleLogger();
        }
    }

    public virtual void PluginManager(IPluginManager manager)
    {
    }

    public virtual void WebSocketConfig(WebSocketFeature feature)
    {
        feature.SetVerifyConnection(VerifyConnection)
            .UseAutoPong(); //当收到ping报文时自动回应pong
    }

    /// <summary>
    /// 停止服务器
    /// </summary>
    public void Stop()
    {
        IsRunning = false;
        _service.Stop();
    }

    /// <summary>
    /// 获取所有客户端
    /// </summary>
    /// <returns></returns>
    public List<HmWebSocketClient> GetClients()
    {
        return _clients.Values.ToList();
    }

    /// <summary>
    /// 根据ID获取客户端
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HmWebSocketClient GetClient(string id)
    {
        return GetClients().FirstOrDefault(client => client.Id == id);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="filter"></param>
    public void SendAll(string text, Func<HmWebSocketClient, bool> filter = null)
    {
        foreach (var client in GetClients())
        {
            if (filter == null || filter(client))
            {
                client.Send(text);
            }
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="filter"></param>
    public void SendAll(byte[] bytes, Func<HmWebSocketClient, bool> filter = null)
    {
        foreach (var client in GetClients())
        {
            if (filter == null || filter(client))
            {
                client.Send(bytes);
            }
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="bytes"></param>
    public void Send(string id, byte[] bytes)
    {
        GetClient(id)?.Send(bytes);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="text"></param>
    public void Send(string id, string text)
    {
        GetClient(id)?.Send(text);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    public void Send(string id, WSDataFrame data)
    {
        GetClient(id)?.Send(data);
    }


    /// <summary>
    /// 启动服务器
    /// </summary>
    /// <param name="certificatePath">证书路径(wss协议必须)</param>
    /// <param name="password">证书密码(wss协议必须)</param>
    /// <param name="sslProtocols">证书协议(wss协议必须)</param>
    public void Start(string certificatePath = null, string password = null,
        SslProtocols sslProtocols = SslProtocols.Tls12)
    {
        _service = new HttpService();
        var config = new TouchSocketConfig();
        if (!string.IsNullOrWhiteSpace(certificatePath))
        {
            config.SetServiceSslOption(new ServiceSslOption() //Ssl配置，当为null的时候，相当于创建了ws服务器，当赋值的时候，相当于wss服务器。
            {
                Certificate = new X509Certificate2(certificatePath!, password),
                SslProtocols = sslProtocols
            });
        }

        _service.Setup(config //加载配置
            .SetListenIPHosts(Port)
            .ConfigureContainer(RegistratorConfig)
            .ConfigurePlugins(manager =>
            {
                //添加WebSocket功能
                WebSocketConfig(manager.UseWebSocket());
                // 添加插件
                foreach (var receivedPlugin in ReceivedPlugins)
                {
                    manager.Add(receivedPlugin);
                }

                PluginAdapter = new WebSocketAdapter()
                {
                    Server = this
                };
                PluginAdapter.OnPreConnected += (client, e) => OnPreConnected?.Invoke(client, e);
                PluginAdapter.OnConnected += (client, e) => OnConnected?.Invoke(client, e);
                PluginAdapter.OnReceived += (client, e) => OnReceived?.Invoke(client, e);
                PluginAdapter.OnClosing += (client, e) => OnClosing?.Invoke(client, e);
                manager.Add(PluginAdapter);
                PluginManager(manager);
            }));
        _service.Start();
        IsRunning = true;
        _service.Logger.Info("服务器已启动");
    }

    private bool VerifyConnection(IHttpSocketClient client, HttpContext context)
    {
        if (!context.Request.IsUpgrade())
        {
            return false;
        }

        if (context.Request.UrlEquals(Url))
        {
            return VerifyHandler(client, context);
        }

        return false;
    }
}