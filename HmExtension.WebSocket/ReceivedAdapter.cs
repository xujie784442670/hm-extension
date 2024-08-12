using System;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace HmExtension.WebSocket;
/// <summary>
/// 消息接收适配器
/// </summary>
public class ReceivedAdapter : PluginBase, IWebSocketReceivedPlugin
{
    public WebSocketServer Server { get; internal set; }

    public HmWebSocketClient Client { get; internal set; }

    public event Action<HmWebSocketClient, WSDataFrame> OnTextReceived;

    public event Action<HmWebSocketClient, WSDataFrame> OnBinaryReceived;

    public async Task OnWebSocketReceived(IWebSocket client, WSDataFrameEventArgs e)
    {
        OnMessageHandler(Server != null ? Server.GetClient(client) : Client, e);
        await e.InvokeNext();
    }

    protected virtual void OnMessageHandler(HmWebSocketClient client, WSDataFrameEventArgs e)
    {
        switch (e.DataFrame.Opcode)
        {
            case WSDataType.Cont:
                OnCont(client, e);
                return;
            case WSDataType.Text:
                if (e.DataFrame.FIN)
                {
                    OnTextReceived?.Invoke(client,e.DataFrame);
                }
                OnText(client, e.DataFrame, e.DataFrame.FIN);
                return;

            case WSDataType.Binary:
                if (e.DataFrame.FIN)
                {
                    OnBinaryReceived?.Invoke(client, e.DataFrame);
                }
                OnBinary(client, e.DataFrame, e.DataFrame.FIN);
                return;

            case WSDataType.Close:
                OnClose(client);
                return;

            case WSDataType.Ping:
                OnPing(client, e);
                return;

            case WSDataType.Pong:
                OnPong(client, e);
                return;
            default:
                break;
        }
    }

    /// <summary>
    /// 接收到中间数据
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    protected virtual void OnCont(HmWebSocketClient client, WSDataFrameEventArgs e)
    {
    }

    /// <summary>
    /// 接收到文本
    /// </summary>
    /// <param name="client">客户端对象</param>
    /// <param name="textBytes">接收到的文本内容直接数组</param>
    /// <param name="isFinal">是否是最后一帧</param>
    protected virtual void OnText(HmWebSocketClient client, WSDataFrame textBytes, bool isFinal)
    {
    }

    /// <summary>
    /// 接收到二进制
    /// </summary>
    /// <param name="client">客户端对象</param>
    /// <param name="byteBlock"></param>
    /// <param name="isFinal">是否是最后一帧</param>
    protected virtual void OnBinary(HmWebSocketClient client, WSDataFrame byteBlock, bool isFinal)
    {
    }

    /// <summary>
    /// 接收到关闭
    /// </summary>
    /// <param name="client"></param>
    protected virtual void OnClose(HmWebSocketClient client)
    {
    }

    /// <summary>
    /// 接收到Ping
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    protected virtual void OnPing(HmWebSocketClient client, WSDataFrameEventArgs e)
    {
    }

    /// <summary>
    /// 接收到Pong
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    protected virtual void OnPong(HmWebSocketClient client, WSDataFrameEventArgs e)
    {
    }
}