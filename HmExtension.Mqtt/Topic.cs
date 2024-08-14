using MQTTnet;
using MQTTnet.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace HmExtension.Mqtt;

/// <summary>
/// 主题操作对象
/// </summary>
public class Topic
{
    /// <summary>
    /// 消息接收事件
    /// </summary>
    public event Action<MqttApplicationMessageReceivedEventArgs> OnApplicationMessageReceived;

    /// <summary>
    /// 主题名称
    /// </summary>
    public string TopicName { get; private set; }

    /// <summary>
    /// 订阅结果,只有第一次订阅时有效
    /// </summary>
    public MqttClientSubscribeResult SubscribeResult { get; internal set; }

    /// <summary>
    /// Mqtt客户端对象
    /// </summary>
    public readonly HmMqttClient Client;

    public Topic(string topicName, HmMqttClient client)
    {
        TopicName = topicName;
        Client = client;
    }

    /// <summary>
    /// 触发消息接收事件
    /// </summary>
    /// <param name="e"></param>
    internal void Dispatch(MqttApplicationMessageReceivedEventArgs e)
    {
        OnApplicationMessageReceived?.Invoke(e);
    }

    /// <summary>
    /// 推送消息给当前主题
    /// </summary>
    /// <param name="data"></param>
    /// <param name="encoding"></param>
    public void Push(string data, Encoding encoding = null)
    {
        Client.Push(TopicName, data, encoding);
    }

    /// <summary>
    /// 推送消息给当前主题
    /// </summary>
    /// <param name="data"></param>
    public void Push(params byte[] data)
    {
        Client.Push(TopicName, data);
    }

    /// <summary>
    /// 推送消息
    /// <para>
    ///  注意: 在此处只能推送当前主题的消息,如果主题不匹配则无法推送
    /// </para>
    /// </summary>
    /// <param name="message"></param>
    public void Push(MqttApplicationMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Topic))
        {
            message.Topic = TopicName;
        }

        if (message.Topic != TopicName) throw new ArgumentException("请勿推送非当前主题的消息");
        Client.Push(message);
    }

    /// <summary>
    /// 取消定订阅
    /// </summary>
    /// <returns></returns>
    public async Task<MqttClientUnsubscribeResult> UnsubscribeAsync()
    {
        return await Client.UnsubscribesAsync(TopicName);
    }
}