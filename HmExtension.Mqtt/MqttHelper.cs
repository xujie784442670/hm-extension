using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;

namespace HmExtension.Mqtt;

public class HmMqttClient
{
    public event Action<ConnectStatus> OnChangeConnected;

    /// <summary>
    /// 全局消息监听
    /// </summary>
    public event Action<MqttApplicationMessageReceivedEventArgs> OnApplicationMessageReceived;


    public IMqttClient Client { get; private set; }
    private readonly Dictionary<string, Topic> _topics = new();

    public string Host { get; set; }
    public int Port { get; set; } = 1883;
    public string Username { get; set; }
    public string Password { get; set; }
    public string ClientId { get; set; }
    public bool IsConnected => Client?.IsConnected ?? false;
    /// <summary>
    /// 是否打印日志,只有通过<see cref="HmMqttClient"/>类中的push方法才能打印,接收消息没有限制
    /// </summary>
    public bool IsPrintLogger { get; set; } = false;

    /// <summary>
    /// 消息接收日志处理
    /// </summary>
    public Action<MqttApplicationMessageReceivedEventArgs> MessageReceivedLoggerHandler= DefaultMessageReceivedLoggerHandler;

    /// <summary>
    /// 消息推送日志处理
    /// </summary>
    public Action<string,MqttApplicationMessage> MessagePushLoggerHandler = DefaultMessagePushLoggerHandler;

    /// <summary>
    /// 创建Mqtt客户端
    /// </summary>
    /// <param name="host"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="port"></param>
    /// <param name="clientId"></param>
    public HmMqttClient(string host = null, string username = null, string password=null, int? port = null, string clientId = null) : this(
        new MqttFactory().CreateMqttClient())
    {
        this.Host = host;
        this.Username = username;
        this.Password = password;
        this.Port = port ?? 1883;
        this.ClientId = clientId ?? Guid.NewGuid().ToString();
    }

    public HmMqttClient(IMqttClient client)
    {
        this.Client = client ?? throw new NullReferenceException("client不能为null");
        InitEventListener();
    }

    private static void DefaultMessageReceivedLoggerHandler(MqttApplicationMessageReceivedEventArgs e)
    {
        var logStr = new StringBuilder();
        logStr.Append($"[接收] {DateTime.Now:yyyy-MM-dd HH:mm:ss} ");
        var msg = e.ApplicationMessage;
        logStr.Append($"[{e.ClientId}]\t[{msg.Topic}]\t");
        logStr.Append($"[ContentType={msg.ContentType}] ");
        logStr.Append($"[Payload={Encoding.UTF8.GetString(msg.PayloadSegment.Array)}] ");
        Console.WriteLine(logStr);
    }

    private static void DefaultMessagePushLoggerHandler(string clientId,MqttApplicationMessage e)
    {
        var logStr = new StringBuilder();
        logStr.Append($"[发送] {DateTime.Now:yyyy-MM-dd HH:mm:ss} ");
        logStr.Append($"[{clientId}]\t[{e.Topic}]\t");
        logStr.Append($"[ContentType={e.ContentType}] ");
        logStr.Append($"[Payload={Encoding.UTF8.GetString(e.PayloadSegment.Array)}] ");
        Console.WriteLine(logStr);
    }

    private void InitEventListener()
    {
        this.Client.ApplicationMessageReceivedAsync += ClientOnApplicationMessageReceivedAsync;
        this.Client.ConnectingAsync += e =>
        {
            OnChangeConnected?.Invoke(ConnectStatus.Connecting);
            return Task.CompletedTask;
        };
        this.Client.ConnectedAsync += e =>
        {
            OnChangeConnected?.Invoke(ConnectStatus.Connected);
            return Task.CompletedTask;
        };
    }

    private Task ClientOnApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        if (IsPrintLogger) MessageReceivedLoggerHandler?.Invoke(e);
        // 触发全局消息
        OnApplicationMessageReceived?.Invoke(e);
        // 触发指定Topic对象
        var msg = e.ApplicationMessage;
        if (_topics.TryGetValue(msg.Topic, out var topic))
        {
            topic.Dispatch(e);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <returns></returns>
    public async Task<MqttClientConnectResult> ConnectAsync()
    {
        return await ConnectAsync(Host, Username, Password, Port, ClientId);
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="host"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="port"></param>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public async Task<MqttClientConnectResult> ConnectAsync(string host, string username, string password,
        int? port = null, string clientId = null)
    {
        this.Host = host;
        this.Username = username;
        this.Password = password;
        this.Port = port ?? 1883;
        this.ClientId = clientId ?? Guid.NewGuid().ToString();
        return await ConnectAsync(
            new MqttClientOptionsBuilder()
                .WithClientId(clientId ?? Guid.NewGuid().ToString())
                .WithTcpServer(host, port)
                .WithCredentials(username, password)
                .WithTlsOptions(new MqttClientTlsOptions()
                {
                    UseTls = false
                }).Build());
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <returns></returns>
    public async Task<MqttClientConnectResult> ConnectAsync(MqttClientOptions options)
    {
        var result = await this.Client.ConnectAsync(options);
        return result;
    }

    /// <summary>
    /// 订阅主题
    /// </summary>
    /// <param name="topicName"></param>
    /// <returns></returns>
    public async Task<Topic> SubscribeAsync(string topicName)
    {
        return (await SubscribesAsync(topicName)).FirstOrDefault();
    }


    /// <summary>
    /// 订阅主题
    /// </summary>
    /// <param name="topics"></param>
    /// <returns></returns>
    public async Task<List<Topic>> SubscribesAsync(params string[] topics)
    {
        var builder = new MqttClientSubscribeOptionsBuilder();
        foreach (var topic in topics)
        {
            builder.WithTopicFilter(topic);
        }

        return await SubscribesAsync(builder.Build());
    }

    /// <summary>
    /// 订阅主题,如果是第一次订阅,则会将结果存放到<see cref="Topic.SubscribeResult"/>中
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<List<Topic>> SubscribesAsync(MqttClientSubscribeOptions options)
    {
        if (!options.TopicFilters.Any()) throw new ArgumentException("需要至少一个主题才能进行订阅");
        var topics = new List<Topic>();
        foreach (var optionsTopicFilter in options.TopicFilters)
        {
            var topicName = optionsTopicFilter.Topic;
            if (!_topics.TryGetValue(topicName, out var topic))
            {
                _topics[topicName] = topic = new Topic(topicName, this);
                topic.SubscribeResult = await Client.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(topicName).Build());
            }

            topics.Add(topic);
        }

        return topics;
    }



    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<MqttClientUnsubscribeResult> UnsubscribesAsync(params string[] topics)
    {
        if (!topics.Any()) throw new ArgumentException("topics不能为空");
        var builder = new MqttClientUnsubscribeOptionsBuilder();
        foreach (var topic in topics)
        {
            builder.WithTopicFilter(topic);
        }
        return await UnsubscribesAsync(builder.Build());
    }

    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<MqttClientUnsubscribeResult> UnsubscribesAsync(MqttClientUnsubscribeOptions options)
    {
        return await this.Client.UnsubscribeAsync(options);
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    /// <param name="reasonString">断开原因</param>
    /// <returns></returns>
    public async Task Disconnected(string reasonString)
    {
        await Disconnected(new MqttClientDisconnectOptionsBuilder()
            .WithReasonString(reasonString).Build());
    }


    /// <summary>
    /// 断开连接
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task Disconnected(MqttClientDisconnectOptions options)
    {
        await this.Client.DisconnectAsync(options);
    }

    /// <summary>
    /// 推送消息
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="data"></param>
    public void Push(string topic, string data, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        Push(topic, encoding.GetBytes(data));
    }

    /// <summary>
    /// 推送消息
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="data"></param>
    public void Push(string topic, params byte[] data)
    {
        Push(new MqttApplicationMessageBuilder()
            .WithPayload(data)
            .WithTopic(topic)
            .Build());
    }

    /// <summary>
    /// 推送消息到指定主题
    /// </summary>
    /// <param name="message"></param>
    public void Push(MqttApplicationMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Topic)) throw new ArgumentException("topic不能为空");
        this.Client.PublishAsync(message);
        if (IsPrintLogger) MessagePushLoggerHandler?.Invoke(ClientId,message);
    }
}

public enum ConnectStatus
{
    // 未连接
    NotConnect,

    // 正在连接
    Connecting,

    // 已连接
    Connected
}