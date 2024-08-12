using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Http;
using TouchSocket.Http.WebSockets;

namespace HmExtension.WebSocket;

public class WebSocketAdapter : PluginBase, IWebSocketHandshakingPlugin,
    IWebSocketHandshakedPlugin, IWebSocketReceivedPlugin, IWebSocketClosingPlugin
{
    public event Action<HmWebSocketClient, HttpContextEventArgs> OnPreConnected;

    public event Action<HmWebSocketClient, HttpContextEventArgs> OnConnected;

    public event Action<HmWebSocketClient, WSDataFrameEventArgs> OnReceived;

    public event Action<HmWebSocketClient, MsgPermitEventArgs> OnClosing;

    public event Action<IPluginManager> OnPluginLoad;

    internal WebSocketServer Server;
    internal HmWebSocketClient Client;

    public HmWebSocketClient GetClient(IWebSocket client)
    {
        return Client ?? Server.GetClient(client);
    }

    public async Task OnWebSocketHandshaking(IWebSocket client, HttpContextEventArgs e)
    {
        var webSocketClient = GetClient(client);
        webSocketClient.HttpContext = e.Context;
        OnPreConnected?.Invoke(webSocketClient, e);
        await e.InvokeNext();
    }

    public async Task OnWebSocketHandshaked(IWebSocket client, HttpContextEventArgs e)
    {
        OnConnected?.Invoke(GetClient(client), e);
        await e.InvokeNext();
    }

    public async Task OnWebSocketReceived(IWebSocket client, WSDataFrameEventArgs e)
    {
        OnReceived?.Invoke(GetClient(client), e);
        await e.InvokeNext();
    }

    public async Task OnWebSocketClosing(IWebSocket client, MsgPermitEventArgs e)
    {
        OnClosing?.Invoke(GetClient(client), e);
        await e.InvokeNext();
    }

    protected override void Loaded(IPluginManager pluginManager)
    {
        this.OnPluginLoad?.Invoke(pluginManager);
    }
}